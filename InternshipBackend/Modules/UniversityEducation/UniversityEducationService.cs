using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IUniversityEducationService : IGenericService<UniversityEducationDTO, UniversityEducationDTO, DeleteRequest, UniversityEducation>
{
}

public class UniversityEducationService(IServiceProvider serviceProvider)
    : GenericService<UniversityEducationDTO, UniversityEducationDTO, DeleteRequest, UniversityEducation>(serviceProvider), IUniversityEducationService
{
}