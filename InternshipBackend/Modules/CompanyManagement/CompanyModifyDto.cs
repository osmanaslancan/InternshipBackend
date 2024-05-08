using InternshipBackend.Data.Models.Enums;

namespace InternshipBackend.Modules.CompanyManagement;

public class CompanyModifyDto
{
    public string? Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? LogoUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? BackgroundPhotoUrl { get; set; }
    public int? CityId { get; set; }
    public int? CountryId { get; set; }
    public string? Sector { get; set; }
    public int NumberOfWorkers { get; set; }
    public string? Description { get; set; }
}
