using InternshipBackend.Core;
using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules;

public interface IUniversityRepository
{
    Task<List<University>> ListAsync();
}

public class UniversityRepository(InternshipDbContext context) : IRepository, IUniversityRepository
{
    public async Task<List<University>> ListAsync()
    {
        return await context.Universities.ToListAsync();
    }
}
