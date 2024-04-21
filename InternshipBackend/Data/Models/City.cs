using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class City : Entity  
{
    public int CountryId { get; set; }
    public required Country Country { get; set; }
    public required string Name { get; set; }
}
