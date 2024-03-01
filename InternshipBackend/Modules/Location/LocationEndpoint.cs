using AutoMapper;
using InternshipBackend.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace InternshipBackend.Modules.Location;

[Route("Location/[action]")]
public class LocationEndpoint(ILocationService locationService, IMapper mapper) 
    : BaseEndpoint
{
    [HttpGet]
    public async Task<List<CountryDTO>> ListCountries()
    {
        var countries = await locationService.ListCountries();
        
        return countries.Select(mapper.Map<CountryDTO>).ToList();
    }

    [HttpGet]
    public async Task<List<CityDTO>> ListCities([FromQuery] int countryId)
    {
        var cities = await locationService.ListCities(countryId);
        
        return cities.Select(mapper.Map<CityDTO>).ToList();
    }
}
