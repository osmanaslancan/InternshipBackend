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
            IsGraduated = false,
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
}