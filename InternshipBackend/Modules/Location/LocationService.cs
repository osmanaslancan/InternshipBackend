using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.Location;

public interface ILocationService : IScopedService
{
    Task<List<City>> ListCities(int countryId);
    Task<List<Country>> ListCountries();
}

public class LocationService(ICityRepository cityRepository, ICountryRepository countryRepository)
    : BaseService, ILocationService
{
    public Task<List<City>> ListCities(int countryId)
    {
        return cityRepository.ListAsync(countryId);
    }

    public Task<List<Country>> ListCountries()
    {
        return countryRepository.ListAsync();
    }
}