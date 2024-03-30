using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IUniversityEducationService : IGenericService<UniversityEducationDto, UniversityEducation>
{
}

public class UniversityEducationService(IServiceProvider serviceProvider)
    : GenericService<UniversityEducationDto, UniversityEducation>(serviceProvider), IUniversityEducationService
{
}