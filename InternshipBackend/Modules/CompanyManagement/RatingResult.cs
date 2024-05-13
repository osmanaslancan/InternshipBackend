namespace InternshipBackend.Modules.CompanyManagement;

public class RatingResult
{
    public required string Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? LogoUrl { get; set; }
    public int CompanyId { get; set; }
    public int NumberOfVotes { get; set; }
    public double Average { get; set; }
}