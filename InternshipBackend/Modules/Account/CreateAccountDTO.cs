namespace InternshipBackend.Modules;

public class CreateAccountDTO
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
}
