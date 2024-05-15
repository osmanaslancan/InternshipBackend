namespace InternshipBackend.Modules.Internship;

public class InternshipPostingCompanyDto
{
    public required int CompanyId { get; set; }
    public required string Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? LogoUrl { get; set; }
    public int NumberOfComments { get; set; }
    public double AveragePoints { get; set; }
    public bool IsCurrentUserFollowing { get; set; }
}