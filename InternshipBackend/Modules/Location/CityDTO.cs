using AutoMapper;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.Location;

[AutoMap(typeof(City))]
public class CityDTO
{
    public int CityId { get; set; }
    public int CountryId { get; set; }
    public required string Name { get; set; }
}
