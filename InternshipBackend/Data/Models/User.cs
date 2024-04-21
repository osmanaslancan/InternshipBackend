#nullable disable
using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class User : Entity
{
    public int Id { get; set; }
    public Guid SupabaseId { get; set; }
    public required string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string ProfilePhotoUrl { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<ForeignLanguage> ForeignLanguages { get; set; } = [];
    public ICollection<UniversityEducation> UniversityEducations { get; set; } = [];
    public ICollection<WorkHistory> Works { get; set; } = [];
    public ICollection<UserProject> UserProjects { get; set; } = [];
    public UserDetail Detail { get; set; }
}
