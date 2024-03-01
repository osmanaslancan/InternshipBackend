namespace InternshipBackend.Data;

public class City
{
    public int CityId { get; set; }
    public int CountryId { get; set; }
    public required Country Country { get; set; }
    public required string Name { get; set; }
}
