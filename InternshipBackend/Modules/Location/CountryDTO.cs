using AutoMapper;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.Location;

[AutoMap(typeof(Country))]
public class CountryDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Code { get; set; }
    public string? Code3 { get; set; }
}
