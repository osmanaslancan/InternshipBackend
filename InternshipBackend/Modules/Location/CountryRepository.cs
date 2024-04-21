using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.Location;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<List<Country>> ListAsync();
}

public class CountryRepository(InternshipDbContext dbContext) 
    : GenericRepository<Country>(dbContext), 
    IGenericRepository<Country>, ICountryRepository
{
    public Task<List<Country>> ListAsync()
    {
        return DbContext.Countries.ToListAsync();
    }
}