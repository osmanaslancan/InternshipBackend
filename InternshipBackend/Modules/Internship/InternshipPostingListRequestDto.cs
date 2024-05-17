using InternshipBackend.Data.Models.Enums;

namespace InternshipBackend.Modules.Internship;

public class InternshipPostingListRequestDto
{
    public int? CompanyId { get; set; }
    public int From { get; set; }
    public int? Take { get; set; }
    public string? MatchQuery { get; set; }
    public WorkType? WorkType { get; set; }
    public EmploymentType? EmploymentType { get; set; }
    public bool? Salary { get; set; }
    public InternshipPostingSort Sort { get; set; }
}