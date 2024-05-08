using InternshipBackend.Core;
using InternshipBackend.Data.Models.Enums;

namespace InternshipBackend.Data.Models;

public class InternshipPosting : CompanyOwnedEntity
{
    public required string Title { get; set; }
    public string? ImageUrl { get; set; }
    public required string Description { get; set; }
    public string? Sector { get; set; }
    public int? CountryId { get; set; }
    public int? CityId { get; set; }
    public string? Location { get; set; }
    public string? Requirements { get; set; }
    public WorkType WorkType { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public bool HasSalary { get; set; }
    public ICollection<InternshipApplication> Applications { get; set; } = [];
    public DateTime DeadLine { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}