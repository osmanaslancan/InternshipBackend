using InternshipBackend.Core;

namespace InternshipBackend.Modules.University;

public interface IUniversityService
{
    Task<List<Data.Models.University>> ListAsync();
}

public class UniversityService(IUniversityRepository universityRepository) : IScopedService, IUniversityService
{
    public Task<List<Data.Models.University>> ListAsync() => universityRepository.ListAsync();
}
