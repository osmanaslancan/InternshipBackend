using System.Reflection;
using System.Security.Claims;
using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using InternshipBackend.Modules.Account;
using InternshipBackend.Modules.App;
using InternshipBackend.Modules.CompanyManagement;
using InternshipBackend.Modules.Internship;
using InternshipBackend.Tests.Mocks;
using InternshipBackend.Tests.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

namespace InternshipBackend.Tests;

public class TestBase
{
    protected record TestContext(InternshipDbContext Db, WebApplication Application, IServiceProvider Services, User CurrentUser, Action<User> ChangeCurrentUser) : IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            await Db.DisposeAsync();
            await Application.DisposeAsync();
        }
    }
    
    protected async Task<TestContext> CreateApplication(Action<WebApplicationBuilder>? configure = null)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(MockGenericRepository<>));
        builder.Services.AddAutoMapper(o =>
        {
            o.AddMaps("InternshipBackend");
        });
        
        builder.Services.AddValidatorsFromAssembly(typeof(InternshipBackendAutoMapperProfile).Assembly);

        builder.Services.AddHttpClient("Supabase");
        builder.Services.AddSingleton<HttpMessageHandlerBuilder, MockHttpMessageHandlerBuilder>();
        
        var user = new User
        {
            Id = 1,
            Email = "test@admin.com",
            SupabaseId = Guid.Parse("6a77ff8461eb485b864ebb3b078657d6"),
        };

        builder.Services.AddSingleton<IHttpContextAccessor>(new MockHttpContextAccessor()
        {
            HttpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(
                        [
                            new Claim(ClaimTypes.NameIdentifier, user.SupabaseId.ToString("N")),
                            new Claim(ClaimTypes.Email, user.Email),
                        ]))
            }
        });
        
        var changeCurrentUser = (User newUser) =>
        {
            user = newUser;
        };
        builder.Services.AddScoped<IUserRetrieverService>((services) => new MockUserRetrieverService()
        {
            GetCurrentUserOrDefaultAction = (edit) => user
        });
        builder.Services.AddSingleton<IDbContextModelCustomizer, SqliteModelCustomizer>();
        builder.Services.AddScoped<InternshipPostingRepository, InternshipPostingRepository>();
        builder.Services.AddScoped<ICompanyService, CompanyService>();
        builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
        builder.Services.AddScoped<IUploadCvService, MockUploadCvService>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddDbContext<InternshipDbContext>(o =>
        {
            o.UseSqlite("Data Source=:memory:");
        }, contextLifetime: ServiceLifetime.Singleton);
            
        configure?.Invoke(builder);
            
        var application = builder.Build();
        using var scope = application.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<InternshipDbContext>();
        await context.Database.OpenConnectionAsync();
        context.GetService<IRelationalDatabaseCreator>().CreateTables();
        context.Users.Add(user);
        await context.SaveChangesAsync();
        
        context.ChangeTracker.Clear();
            
        return new TestContext(context, application, application.Services.CreateScope().ServiceProvider, user, changeCurrentUser);
    }

}