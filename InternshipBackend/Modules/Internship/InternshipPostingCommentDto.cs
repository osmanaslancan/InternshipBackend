namespace InternshipBackend.Modules.Internship;

public class InternshipPostingCommentDto
{
    public string? UserName { get; set; }
    public string? UserSurname { get; set; }
    public string? PhotoUrl { get; set; }
    public required int UserId { get; set; }
    public required string Comment { get; set; }
    public required int Points { get; set; }
    public required DateTime CreatedAt { get; set; }
}