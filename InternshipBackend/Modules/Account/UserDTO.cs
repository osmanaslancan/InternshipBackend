#nullable disable
using AutoMapper;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.Account;

[AutoMap(typeof(User))]
public class UserDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string ProfilePhotoUrl { get; set; }
    public string PhoneNumber { get; set; }

    public IEnumerable<ForeignLanguageListDto> ForeignLanguages { get; set; } = [];
    public IEnumerable<UniversityEducationListDto> UniversityEducations { get; set; } = [];
    public IEnumerable<WorkHistoryListDto> Works { get; set; } = [];
    public IEnumerable<UserProjectDtoListDto> UserProjects { get; set; } = [];
    public UserDetailDTO Detail { get; set; }
}
