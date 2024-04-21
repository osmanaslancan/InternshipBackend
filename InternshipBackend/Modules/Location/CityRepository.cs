using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.Location;

public interface ICityRepository : IGenericRepository<Country>
{
    Task<List<City>> ListAsync(int countryId);
}

public class CityRepository(InternshipDbContext dbContext)
    : GenericRepository<Country>(dbContext),
    IGenericRepository<Country>, ICityRepository
{
    public Task<List<City>> ListAsync(int countryId)
    {
        return DbContext.Cities.Where(x => x.CountryId == countryId).ToListAsync();
    }
}