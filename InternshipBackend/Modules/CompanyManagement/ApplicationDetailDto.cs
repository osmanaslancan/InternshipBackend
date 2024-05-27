using InternshipBackend.Modules.ForeignLanguage;
using InternshipBackend.Modules.UniversityEducations;
using InternshipBackend.Modules.UserDetails;
using InternshipBackend.Modules.UserProjects;
using InternshipBackend.Modules.WorkHistory;

namespace InternshipBackend.Modules.CompanyManagement;

public class ApplicationDetailDto
{
    public int Id { get; set; }
    public int InternshipPostingId { get; set; }
    public string? Message { get; set; }
    public string? CvUrl { get; set; }

    public int UserId { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public string? PhoneNumber { get; set; }

    public ICollection<ForeignLanguageListDto> ForeignLanguages { get; set; } = [];
    public ICollection<UniversityEducationListDto> UniversityEducations { get; set; } = [];
    public ICollection<WorkHistoryListDto> Works { get; set; } = [];
    public ICollection<UserProjectListDto> Projects { get; set; } = [];
    public ICollection<UserReferenceListDto> References { get; set; } = [];
    public required UserDetailDto Detail { get; set; }
}