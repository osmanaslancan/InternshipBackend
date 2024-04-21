using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Modules.UniversityEducations;

public interface IUniversityEducationService : IGenericEntityService<UniversityEducationModifyDto, UniversityEducation>
{
}

public class UniversityEducationService(IServiceProvider serviceProvider)
    : GenericEntityService<UniversityEducationModifyDto, UniversityEducation>(serviceProvider), IUniversityEducationService
{
}