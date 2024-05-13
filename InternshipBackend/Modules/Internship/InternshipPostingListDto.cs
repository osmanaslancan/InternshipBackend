using InternshipBackend.Modules.CompanyManagement;

namespace InternshipBackend.Modules.Internship;

public class InternshipPostingListDto : InternshipPostingModifyDto
{
    public int Id { get; set; }
    public required InternshipPostingCompanyDto Company { get; set; }
}
