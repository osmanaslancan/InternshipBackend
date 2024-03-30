using InternshipBackend.Core;

namespace InternshipBackend.Data;

public class City : IHasIdField
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public required Country Country { get; set; }
    public required string Name { get; set; }
}
