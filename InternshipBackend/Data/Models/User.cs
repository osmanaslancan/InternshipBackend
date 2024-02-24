namespace InternshipBackend.Data;

public class User
{
    public int UserId { get; set; }
    public Guid SupabaseId { get; set; }
    public required string Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? ProfilePhotoUrl { get; set; }
}
