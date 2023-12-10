using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.machine.json", true);

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


builder.Services.AddControllers(o =>
{
    o.Filters.Add<ExceptionFilter>(0);
}).AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
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

});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        var test = app;
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


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors();


app.UseAuthorization();

app.MapControllers();

app.Run();
