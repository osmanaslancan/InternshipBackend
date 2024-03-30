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
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.machine.json", true);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<InternshipDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Supabase")));


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
    });


builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var serviceTypes = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && typeof(IService).IsAssignableFrom(t))
    .ToList();

foreach (var serviceType in serviceTypes)
{
    var interfaceTypes = serviceType.GetInterfaces().Where(x => x != typeof(IService));
    foreach (var interfaceType in interfaceTypes)
    {
        builder.Services.AddScoped(interfaceType, serviceType);
    }
}


var repositoryTypes = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && typeof(IRepository).IsAssignableFrom(t))
    .ToList();

foreach (var repositoryType in repositoryTypes)
{
    var interfaceTypes = repositoryType.GetInterfaces().Where(x => x != typeof(IRepository));
    foreach (var interfaceType in interfaceTypes)
    {
        builder.Services.AddScoped(interfaceType, repositoryType);
    }
}


builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

builder.Services.AddAutoMapper(o => 
{
    o.CreateMap<string, DriverLicense>()
        .ForMember(x => x.License, x => x.MapFrom((src, dest) => src));

}, typeof(Program));

builder.Services.AddControllers(o =>
{
    o.Filters.Add<ExceptionFilter>(0);
}).AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

builder.Services.AddHttpClient("Supabase", o =>
{
    o.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", builder.Configuration["SupabaseAdminKey"]);
});

builder.Services.AddRequestLocalization(o =>
{
    o.DefaultRequestCulture = new RequestCulture("tr-TR");
    o.SupportedCultures =
    [
        new CultureInfo("en-US"),
        new CultureInfo("tr-TR"),
    ];
    o.SupportedUICultures =
    [
        new CultureInfo("en-US"),
        new CultureInfo("tr-TR"),
    ];
    
    o.RequestCultureProviders =
    [
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider() 
        {
            CookieName = "culture"
        },
        new AcceptLanguageHeaderRequestCultureProvider()
    ];
});

builder.Services.AddLocalization();

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

context.Database.ExecuteSqlRaw(query);

scope.Dispose();

#endregion

await new SeederManager().ExecuteAsync(app.Services);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseRequestLocalization();
app.UseCors();


app.UseAuthorization();

app.MapControllers();

app.Run();
