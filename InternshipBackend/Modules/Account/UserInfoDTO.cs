namespace InternshipBackend.Modules;

public class UserInfoDTO
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public int? Age { get; set; }
    public string? UniversityName { get; set; }
}

public class UserInfoUpdateDTO
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public int? Age { get; set; }
    public string? UniversityName { get; set; }
}