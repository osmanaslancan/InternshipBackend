using InternshipBackend.Core;
using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.University;

public interface IUniversityRepository
{
    Task<List<Data.Models.University>> ListAsync();
}

public class UniversityRepository(InternshipDbContext context) : IRepository, IUniversityRepository
{
    public async Task<List<Data.Models.University>> ListAsync()
    {
        return await context.Universities.ToListAsync();
    }
}
