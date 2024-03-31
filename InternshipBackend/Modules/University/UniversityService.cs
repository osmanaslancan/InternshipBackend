using InternshipBackend.Core;
using InternshipBackend.Data;

namespace InternshipBackend.Modules;

public interface IUniversityService
{
    Task<List<University>> ListAsync();
}

public class UniversityService(IUniversityRepository universityRepository) : IScopedService, IUniversityService
{
    public Task<List<University>> ListAsync() => universityRepository.ListAsync();
}
