namespace InternshipBackend.Modules.Internship;

public class InternshipCommentDto
{
    public int InternshipPostingId { get; set; }
    public string? Comment { get; set; }
    public int Points { get; set; }
}