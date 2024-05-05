#nullable disable
using AutoMapper;
using InternshipBackend.Data;
using InternshipBackend.Modules.ForeignLanguage;
using InternshipBackend.Modules.UniversityEducations;
using InternshipBackend.Modules.UserDetail;
using InternshipBackend.Modules.UserProjects;
using InternshipBackend.Modules.WorkHistory;

namespace InternshipBackend.Modules.Account;

public class UserDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string ProfilePhotoUrl { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<ForeignLanguageListDto> ForeignLanguages { get; set; } = [];
    public ICollection<UniversityEducationListDto> UniversityEducations { get; set; } = [];
    public ICollection<WorkHistoryListDto> Works { get; set; } = [];
    public ICollection<UserProjectListDto> UserProjects { get; set; } = [];
    public UserDetailDTO Detail { get; set; }
}
