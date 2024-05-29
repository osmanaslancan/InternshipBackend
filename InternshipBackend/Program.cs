using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Core.Seed;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using InternshipBackend.Data.Seeds;
using InternshipBackend.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using InternshipBackend;
using InternshipBackend.Core.Authorization;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models.Enums;
using InternshipBackend.Modules.Account.Authorization;
using InternshipBackend.Modules.App;
using Microsoft.AspNetCore.Authorization;
using OpenAI.Extensions;
using OpenAI.ObjectModels;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.machine.json", true);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<InternshipDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration["SupabaseConnectionString"]);
    options.EnableDetailedErrors();
});

builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        var signingKey = builder.Configuration["SupabaseSigningKey"] ?? throw new ArgumentException("IssuerSigningKey");

        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = "https://vzmyswxvnmseubtqgjpc.supabase.co/auth/v1",
            ValidAudience = "authenticated",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
        };

        o.Events = new JwtBearerEvents()
        {
            OnTokenValidated = (context) =>
            {
                var data = context.Principal?.FindFirstValue("app_metadata");
                if (data is null)
                {
                    context.Fail("Unauthorized missing claims");
                    return Task.CompletedTask;
                }

                var appMetadata = JsonSerializer.Deserialize<Dictionary<string, object>>(data);

                int? userType =
                    appMetadata?.GetValueOrDefault("user_type") is JsonElement
                    {
                        ValueKind: JsonValueKind.Number
                    } element
                        ? element.GetInt32()
                        : null;
                var userName = appMetadata?.GetValueOrDefault("user_name");
                var userSurname = appMetadata?.GetValueOrDefault("user_surname");

                if (userType is null ||
                    (userType == (int)AccountType.Intern && (userName is null || userSurname is null)))
                {
                    if (context.Request.Path.Value is "/Account/UpdateUserInfo" or "/Account/IsUserRegistered"
                        or "/Account/GetInfo")
                        return Task.CompletedTask;

                    context.Fail("Unauthorized missing claims");
                    return Task.CompletedTask;
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddTransient<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
builder.Services.AddTransient<IPermissionDefinitionManager, PermissionDefinitionManager>();
builder.Services.AddTransient<IPermissionDefinitionProvider, PermissionDefinitionProvider>();
builder.Services.AddTransient<IAuthorizationHandler, PermissionRequirementHandler>();
builder.Services.AddTransient<IAuthorizationHandler, UserTypeRequirementHandler>();


var typeSourceProvider = new TypeSourceProvider(typeof(Program).Assembly);

builder.Services.AddSingleton<ITypeSourceProvider>(typeSourceProvider);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var serviceTypes = typeSourceProvider.FindTypesInheritedFrom<IScopedService>();
builder.Services.RegisterForInterfacesAndSelf(ServiceLifetime.Scoped, serviceTypes, [typeof(IScopedService)]);

serviceTypes = typeSourceProvider.FindTypesInheritedFrom<ISingletonService>();
builder.Services.RegisterForInterfacesAndSelf(ServiceLifetime.Singleton, serviceTypes, [typeof(ISingletonService)]);

var repositoryTypes = typeSourceProvider.FindTypesInheritedFrom<IRepository>();
builder.Services.RegisterForInterfacesAndSelf(ServiceLifetime.Scoped, repositoryTypes, [typeof(IRepository)]);

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(policy =>
    {
        policy.SetIsOriginAllowed(
                origin => new Uri(origin).Host is "10.0.2.2" or "localhost" or "stajbuldum.osman.tech").AllowAnyMethod()
            .AllowAnyHeader().AllowCredentials();
        // policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        // policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

builder.Services.AddAutoMapper(o => { o.AddProfile<InternshipBackendAutoMapperProfile>(); }, typeof(Program));

builder.Services.AddControllers(o => { o.Filters.Add<ExceptionFilter>(0); }).AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    o.JsonSerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var scheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
    };

    o.AddSecurityDefinition("Bearer", scheme);

    o.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        },
    });

    // Add all xml doc files to swagger generator.
    var xmlFiles = Directory.GetFiles(
        AppContext.BaseDirectory,
        "*.xml",
        SearchOption.TopDirectoryOnly);

    foreach (var xmlFile in xmlFiles)
    {
        o.IncludeXmlComments(xmlFile);
    }
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("Supabase",
    o =>
    {
        o.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", builder.Configuration["SupabaseAdminKey"]);
    });

builder.Services.AddRequestLocalization(o =>
{
    o.DefaultRequestCulture = new RequestCulture("tr-TR");
    o.SupportedCultures =
    [
        new CultureInfo("tr"),
        new CultureInfo("tr-TR"),
        new CultureInfo("en"),
        new CultureInfo("en-US"),
    ];
    o.SupportedUICultures =
    [
        new CultureInfo("tr"),
        new CultureInfo("tr-TR"),
        new CultureInfo("en"),
        new CultureInfo("en-US"),
    ];

    o.RequestCultureProviders =
    [
        new QueryStringRequestCultureProvider(),
        new UserCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    ];
});

builder.Services.AddOpenAIService(o =>
{
    o.ApiKey = builder.Configuration["OpenAISecret"] ?? throw new ArgumentNullException("OpenAISecret");
});

builder.Services.AddLocalization();

builder.Services.AddQuartz(q =>
{
    // Just use the name of your job that you created in the Jobs folder.
    var jobKey = new JobKey("NotificationSendJob");
    q.AddJob<NotificationSendJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("NotificationSendJob-trigger")
        .WithCronSchedule("0 * * ? * *")
    );
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new ServiceResponse
        {
            Error = new ServiceError
            {
                Name = "UnknownError",
                Details = ex.Message
            }
        });
    }
});


var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<InternshipDbContext>();
context.Database.Migrate();
var script = context.Database.GenerateCreateScript();

#region Enable Row Level Security

var tableNames = context.Model.GetEntityTypes()
    .Select(t => t.GetTableName())
    .Where(t => !t!.StartsWith("storage."))
    .Distinct()
    .ToList();
var query = "";
foreach (var tableName in tableNames)
{
    query += "alter table \"" + tableName + "\" enable row level security;\n";
}

query += "alter table" + "\"__EFMigrationsHistory\"" + " enable row level security;\n";

query += """
         DO $$
         BEGIN
           IF NOT EXISTS (
              select 1 from pg_policies
              where policyname = 'Enable access for supabase auth admin'
           ) THEN
             -- Apply the policy
             CREATE POLICY "Enable access for supabase auth admin"
             ON "public"."Users"
             AS PERMISSIVE
             FOR SELECT
             TO supabase_auth_admin
             USING (true);
           END IF;
         END$$;
         """;
// //Access token hook
// query += """
// create or replace function public.custom_access_token_hook(event jsonb) returns jsonb language plpgsql as $$
// declare
// claims jsonb;
// user_type int4;
// user_name text;
// user_surname text;
// begin
//     -- Check if the user is marked as admin in the profiles table
//     SELECT "AccountType", "Name", "Surname" into user_type, user_name, user_surname from public."Users" where "SupabaseId" = (event->>'user_id')::uuid;
//
// claims := event->'claims';
//
// -- Check if 'app_metadata' exists in claims
// if jsonb_typeof(claims->'app_metadata') is null then
// -- If 'app_metadata' does not exist, create an empty object
// claims := jsonb_set(claims, '{app_metadata}', '{}');
// end if;
//     
// if user_type is null then
// claims := jsonb_set(claims, '{app_metadata, user_type}', 'null');
// else
// claims := jsonb_set(claims, '{app_metadata, user_type}', user_type::text::jsonb);
// end if;
//
// if user_name is null then
// claims := jsonb_set(claims, '{app_metadata, user_name}', 'null');
// else
// claims := jsonb_set(claims, '{app_metadata, user_name}', to_jsonb(user_name));
// end if;
//
// if user_surname is null then
// claims := jsonb_set(claims, '{app_metadata, user_surname}', 'null');
// else
// claims := jsonb_set(claims, '{app_metadata, user_surname}', to_jsonb(user_surname));
// end if;
//     
// -- Update the 'claims' object in the original event
// event := jsonb_set(event, '{claims}', claims);
//
// -- Return the modified or original event
// return event;
// end;
// $$;
//
// grant execute
// on function public.custom_access_token_hook
// to supabase_auth_admin;
// grant all
// on table public."Users"
// to supabase_auth_admin;

// """;

await context.Database.ExecuteSqlRawAsync(query);
scope.Dispose();

#endregion

await new SeederManager().ExecuteAsync(app.Services);
if (app.Environment.IsDevelopment())
{
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile("firebase-credentials.json")
    });
}
else
{
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromJson(app.Configuration["FirebaseCredentials"])
    });
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseRequestLocalization();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();