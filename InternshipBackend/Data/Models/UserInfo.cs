namespace InternshipBackend.Data;

public class UserInfo
{
    public int UserInfoId { get; set; }
    public Guid SupabaseId { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public int? Age { get; set; }
    public int? UniversityId { get; set; }
    public University? University { get; set; }
    public ICollection<UserProject>? Projects { get; set; }
}
