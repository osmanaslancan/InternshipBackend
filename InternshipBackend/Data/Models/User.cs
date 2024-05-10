#nullable disable
using System.ComponentModel.DataAnnotations;
using InternshipBackend.Core;
using InternshipBackend.Data.Models.Enums;

namespace InternshipBackend.Data.Models;

public class User : Entity
{
    public Guid SupabaseId { get; set; }
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    [DataType(DataType.ImageUrl)]
    public string ProfilePhotoUrl { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    public AccountType AccountType { get; set; }

    public ICollection<ForeignLanguage> ForeignLanguages { get; set; } = [];
    public ICollection<UniversityEducation> UniversityEducations { get; set; } = [];
    public ICollection<WorkHistory> Works { get; set; } = [];
    public ICollection<UserProject> Projects { get; set; } = [];
    public ICollection<UserPermission> UserPermissions { get; set; } = [];
    public ICollection<UserReference> References { get; set; } = [];
    public UserDetail Detail { get; set; }
}
