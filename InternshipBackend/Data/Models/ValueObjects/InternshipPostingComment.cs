namespace InternshipBackend.Data.Models.ValueObjects;

public class InternshipPostingComment
{
    public required int UserId { get; set; }
    public required string Comment { get; set; }
    public required int Points { get; set; }
    public required DateTime CreatedAt { get; set; }
}