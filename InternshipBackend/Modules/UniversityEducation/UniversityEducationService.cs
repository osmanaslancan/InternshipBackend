using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IUniversityEducationService : IGenericEntityService<UniversityEducationDto, UniversityEducation>
{
}

public class UniversityEducationService(IServiceProvider serviceProvider)
    : GenericEntityService<UniversityEducationDto, UniversityEducation>(serviceProvider), IUniversityEducationService
{
}