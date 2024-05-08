
using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class Company : Entity
{
    public int? AdminUserId { get; set; }
    public User? AdminUser { get; set; }
    public ICollection<CompanyEmployee> Employees { get; set; } = [];
    public bool IsVerified { get; set; }
    public required string Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? LogoUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? BackgroundPhotoUrl { get; set; }
    public int? CityId { get; set; }
    public int? CountryId { get; set; }
    public string? Sector { get; set; }
    public int NumberOfWorkers { get; set; }
    public required string Description { get; set; }
}
