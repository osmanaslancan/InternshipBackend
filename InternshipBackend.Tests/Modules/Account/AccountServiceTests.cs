using System.Security.Claims;
using FluentValidation;
using InternshipBackend.Data.Models;
using InternshipBackend.Modules.Account;
using InternshipBackend.Resources.Error;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipBackend.Tests.Modules.Account;

[TestSubject(typeof(AccountService))]
public class AccountServiceTests : TestBase
{
    [Fact]
    public async Task Creates_Valid_Account()
    {
        await using var application = await CreateApplication(x =>
        {
            x.Services.AddTransient<IAccountRepository, AccountRepository>();
        });

        application.Db.Users.Remove(application.CurrentUser);
        await application.Db.SaveChangesAsync();

        var service = ActivatorUtilities.CreateInstance<AccountService>(application.Services);

        var dto = new UserInfoUpdateDto()
        {
            Name = "Test",
            Surname = "Test",
            PhoneNumber = "123456789"
        };

        await service.CreateAsync(dto);

        var users = await application.Db.Users.ToListAsync();

        Assert.Single(users);
        var user = users.First();
        Assert.Equal(dto.Name, user.Name);
        Assert.Equal(dto.Surname, user.Surname);
        Assert.Equal(dto.PhoneNumber, user.PhoneNumber);
    }

    [Fact]
    public async Task Create_Throws_If_Email_Not_Found()
    {
        await using var application = await CreateApplication(x =>
        {
            x.Services.AddTransient<IAccountRepository, AccountRepository>();
        });

        application.Services.GetRequiredService<IHttpContextAccessor>()!.HttpContext!.User = new ClaimsPrincipal(
            new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, application.CurrentUser.SupabaseId.ToString("N"))]));

        var service = ActivatorUtilities.CreateInstance<AccountService>(application.Services);

        var dto = new UserInfoUpdateDto()
        {
            Name = "Test",
            Surname = "Test",
            PhoneNumber = "123456789"
        };

        var exception = await Assert.ThrowsAsync<ValidationException>(async () => await service.CreateAsync(dto));
        Assert.Equal(ErrorCodes.EmailNotFound, exception.Message);
    }
    
    [Fact]
    public async Task Create_Throws_If_Email_Exists()
    {
        await using var application = await CreateApplication(x =>
        {
            x.Services.AddTransient<IAccountRepository, AccountRepository>();
        });

        var service = ActivatorUtilities.CreateInstance<AccountService>(application.Services);

        var dto = new UserInfoUpdateDto()
        {
            Name = "Test",
            Surname = "Test",
            PhoneNumber = "123456789"
        };

        var exception = await Assert.ThrowsAsync<ValidationException>(async () => await service.CreateAsync(dto));
        Assert.Equal(ErrorCodes.EmailExists, exception.Message);
    }
    
    [Fact]
    public async Task Create_Throws_If_User_Exists()
    {
        await using var application = await CreateApplication(x =>
        {
            x.Services.AddTransient<IAccountRepository, AccountRepository>();
        });

        application.Services.GetRequiredService<IHttpContextAccessor>()!.HttpContext!.User = new ClaimsPrincipal(
            new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, application.CurrentUser.SupabaseId.ToString("N")),
                new Claim(ClaimTypes.Email, "notthesame@internship.backend")
            ]));
        
        var service = ActivatorUtilities.CreateInstance<AccountService>(application.Services);

        var dto = new UserInfoUpdateDto()
        {
            Name = "Test",
            Surname = "Test",
            PhoneNumber = "123456789"
        };

        var exception = await Assert.ThrowsAsync<ValidationException>(async () => await service.CreateAsync(dto));
        Assert.Equal(ErrorCodes.UserAlreadyExists, exception.Message);
    }
    
    [Fact]
    public async Task GetCurrentUserInfoOrDefault_Retrieves_CurrentUser_If_LoggedIn()
    {
        await using var application = await CreateApplication(x =>
        {
            x.Services.AddTransient<IAccountRepository, AccountRepository>();
        });
      
        
        var service = ActivatorUtilities.CreateInstance<AccountService>(application.Services);

        var user = await service.GetCurrentUserInfoOrDefault();
        
        Assert.Equal(application.CurrentUser.Id, user.Id);
        Assert.Equal(application.CurrentUser.Name, user.Name);
        Assert.Equal(application.CurrentUser.Surname, user.Surname);
        Assert.Equal(application.CurrentUser.PhoneNumber, user.PhoneNumber);
    }
    
    [Fact]
    public async Task GetCurrentUserInfoOrDefault_Retrieves_NullUser_If_Not_Logged_In()
    {
        await using var application = await CreateApplication(x =>
        {
            x.Services.AddTransient<IAccountRepository, AccountRepository>();
        });
      
        application.Services.GetRequiredService<IHttpContextAccessor>()!.HttpContext!.User = new ClaimsPrincipal(
            new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "4a7631a1443d4423bde9e74974e989b5")]));
        
        var service = ActivatorUtilities.CreateInstance<AccountService>(application.Services);

        var user = await service.GetCurrentUserInfoOrDefault();
        
        Assert.Null(user);
    }
}