using InternshipBackend.Modules.Account;
using InternshipBackend.Modules.UniversityEducations;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipBackend.Tests.Modules.UniversityEducations;

[TestSubject(typeof(UniversityEducationService))]
public class UniversityEducationServiceTests : TestBase
{

    [Fact]
    public async Task Creates_Valid_Record()
    {
        await using var application = await CreateApplication(x =>
        {
            x.Services.AddTransient<IUniversityEducationService, UniversityEducationService>();
        });

        var service = application.Services.GetRequiredService<IUniversityEducationService>();

        var dto = new UniversityEducationModifyDto()
        {
            Faculty = "Test",
            Department = "Test",
            StartDate = DateTime.Now.Date,
            EducationYear = 2,
            IsGraduated = false,
            UniversityName = "test",
        };
        
        await service.CreateAsync(dto);
        application.Db.ChangeTracker.Clear();
        Assert.Single(application.Db.UniversityEducations);
        var universityEducation = application.Db.UniversityEducations.First();
        Assert.Equal("Test", universityEducation.Faculty);
        Assert.Equal("Test", universityEducation.Department);
        Assert.False(universityEducation.IsGraduated);
        Assert.Equal(dto.StartDate, universityEducation.StartDate);
    }
    
    [Fact]
    public async Task Updates_Record_With_Valid_Input()
    {
        await using var application = await CreateApplication(x =>
        {
            x.Services.AddTransient<IUniversityEducationService, UniversityEducationService>();
        });

        var service = application.Services.GetRequiredService<IUniversityEducationService>();

        var createDto = new UniversityEducationModifyDto()
        {
            Faculty = "Test",
            Department = "Test",
            StartDate = DateTime.Now.Date,
            IsGraduated = false,
            EducationYear = 2,
            UniversityName = "test",
        };

        var createdRecord = await service.CreateAsync(createDto);
        application.Db.ChangeTracker.Clear();

        var updateDto = new UniversityEducationModifyDto()
        {
            Faculty = "Updated",
            Department = "Updated",
            StartDate = DateTime.Now.Date,
            EndDate = DateTime.Now.Date.Add(TimeSpan.FromDays(22)),
            IsGraduated = true,
            UniversityName = "test",
        };

        await service.UpdateAsync(createdRecord.Id, updateDto);
        application.Db.ChangeTracker.Clear();

        var updatedRecord = application.Db.UniversityEducations.First();
        Assert.Equal("Updated", updatedRecord.Faculty);
        Assert.Equal("Updated", updatedRecord.Department);
        Assert.True(updatedRecord.IsGraduated);
        Assert.Equal(updateDto.StartDate, updatedRecord.StartDate);
    }

    [Fact]
    public async Task Deletes_Record_With_Valid_Id()
    {
        await using var application = await CreateApplication(x =>
        {
            x.Services.AddTransient<IUniversityEducationService, UniversityEducationService>();
        });

        var service = application.Services.GetRequiredService<IUniversityEducationService>();

        var createDto = new UniversityEducationModifyDto()
        {
            Faculty = "Test",
            Department = "Test",
            StartDate = DateTime.Now.Date,
            IsGraduated = false,
            EducationYear = 2,
            UniversityName = "test",
        };

        var createdRecord = await service.CreateAsync(createDto);
        application.Db.ChangeTracker.Clear();

        await service.DeleteAsync(createdRecord.Id);
        application.Db.ChangeTracker.Clear();

        Assert.Empty(application.Db.UniversityEducations);
    }
}