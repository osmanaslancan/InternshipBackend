namespace InternshipBackend.Modules;

public class CreateUserInfoDTO
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public int? Age { get; set; }
    public int? UniversityId { get; set; }
}
