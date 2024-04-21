using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using InternshipBackend.Modules.UniversityEducations;
using InternshipBackend.Tests.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace InternshipBackend.Tests;

public class GenericEntityServiceTests : TestBase
{
    [Fact]
    public async Task Create_Adds_UserId_When_Entity_Has_UserId()
    {
        await using var testContext = await CreateApplication();
        using var scope = testContext.Application.Services.CreateScope();
            
        var service = new UniversityEducationService(scope.ServiceProvider);
            
        await service.CreateAsync(new UniversityEducationModifyDto
        {
            Department = "Computer Science",
            StartDate = DateTime.Now,
            UniversityId = null,
        });
            
        var record = testContext.Db.UniversityEducations.FirstOrDefault(x => x.Department == "Computer Science");
        Assert.NotNull(record);
        Assert.Equal(testContext.CurrentUser.Id, record.UserId);
    }
        
    [Fact]
    public async Task Prevents_Changing_Other_Users_Data()
    {
        await using var testContext = await CreateApplication();
        var newUser = new User { Id = 2, Email = "test", SupabaseId = Guid.NewGuid()};
        testContext.Db.Users.Add(newUser);
        using var scope = testContext.Application.Services.CreateScope();
            
        var service = new UniversityEducationService(scope.ServiceProvider);

        var data = new UniversityEducationModifyDto
        {
            Department = "Computer Science",
        };
            
        var createdRecord = await service.CreateAsync(data);
            
        testContext.ChangeCurrentUser(newUser);

        var exp = await Assert.ThrowsAsync<Exception>(async () =>
        {
            await service.UpdateAsync(createdRecord.Id, new UniversityEducationModifyDto()
            {
                Department = "Changed Department",
            });
        });
            
        Assert.Equal("You can't update other user's data", exp.Message);
    }
        
    [Fact]
    public async Task Prevents_Deleting_Other_Users_Data()
    {
        await using var testContext = await CreateApplication();
        var newUser = new User { Id = 2, Email = "test", SupabaseId = Guid.NewGuid()};
        testContext.Db.Users.Add(newUser);
        using var scope = testContext.Application.Services.CreateScope();
            
        var service = new UniversityEducationService(scope.ServiceProvider);

        var data = new UniversityEducationModifyDto
        {
            Department = "Computer Science",
        };
            
        var createdRecord = await service.CreateAsync(data);
            
        testContext.ChangeCurrentUser(newUser);

        var exp = await Assert.ThrowsAsync<Exception>(async () =>
        {
            await service.DeleteAsync(createdRecord.Id);
        });
            
        Assert.Equal("You can't delete other user's data", exp.Message);
    }
        
    [Fact]
    public async Task Validates_Dto_On_Create()
    {
        await using var testContext = await CreateApplication(builder =>
        {
            var validator = new MockValidator<UniversityEducationModifyDto>();
            validator.RuleFor(x => x.Department).MaximumLength(5);
            builder.Services.AddSingleton<IValidator<UniversityEducationModifyDto>>(validator);
        });
            
        using var scope = testContext.Application.Services.CreateScope();
            
        var service = new UniversityEducationService(scope.ServiceProvider);
            
        var data = new UniversityEducationModifyDto
        {
            Department = "Length More than 5",
        };

        await Assert.ThrowsAsync<ValidationException>(async () => await service.CreateAsync(data));
        await Assert.ThrowsAsync<ValidationException>(async () => await service.UpdateAsync(0, data));
    }
        
    [Fact]
    public async Task Creates_When_Everything_Is_Valid()
    {
        await using var testContext = await CreateApplication();
            
        using var scope = testContext.Application.Services.CreateScope();
            
        var service = new UniversityEducationService(scope.ServiceProvider);
            
        var data = new UniversityEducationModifyDto
        {
            Department = "Computer Science",
        };

        await service.CreateAsync(data);
            
        var record = testContext.Db.UniversityEducations.FirstOrDefault(x => x.Department == "Computer Science");
        Assert.NotNull(record);
    }
        
    [Fact]
    public async Task Updates_When_Everything_Is_Valid()
    {
        await using var testContext = await CreateApplication();
            
        using var scope = testContext.Application.Services.CreateScope();
            
        var service = new UniversityEducationService(scope.ServiceProvider);
            
        var old = testContext.Db.UniversityEducations.Add(new UniversityEducation
        {
            Department = "Not Computer Science",
            UserId = testContext.CurrentUser.Id,
            StartDate = DateTime.Now,
        });
        await testContext.Db.SaveChangesAsync();
            
        old.State = EntityState.Detached;
            
        var data = new UniversityEducationModifyDto
        {
            Department = "Computer Science",
        };

        await service.UpdateAsync(old.Entity.Id, data);
            
        var record = testContext.Db.UniversityEducations.FirstOrDefault(x => x.Department == "Computer Science");
        Assert.NotNull(record);
    }
        
    [Fact]
    public async Task Deletes_When_Everything_Is_Valid()
    {
        await using var testContext = await CreateApplication();
            
        using var scope = testContext.Application.Services.CreateScope();
            
        var service = new UniversityEducationService(scope.ServiceProvider);
            
        var old = testContext.Db.UniversityEducations.Add(new UniversityEducation
        {
            Department = "Computer Science",
            UserId = testContext.CurrentUser.Id,
            StartDate = DateTime.Now,
        });
            
        await testContext.Db.SaveChangesAsync();

        old.State = EntityState.Detached;
            
        Assert.NotEmpty(testContext.Db.UniversityEducations.AsNoTracking().ToList());
            
        await service.DeleteAsync(old.Entity.Id);
            
        var record = testContext.Db.UniversityEducations.ToList();
        Assert.Empty(record);
    }
}