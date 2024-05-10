using InternshipBackend.Data.Models;
using InternshipBackend.Data.Models.Enums;

namespace InternshipBackend.Modules.CompanyManagement;

public class InternshipPostingModifyDto
{
    public string? Title { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public string? Sector { get; set; }
    public int? CountryId { get; set; }
    public int? CityId { get; set; }
    public string? Location { get; set; }
    public string? Requirements { get; set; }
    public WorkType WorkType { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public bool HasSalary { get; set; }
    public DateTime DeadLine { get; set; }
}