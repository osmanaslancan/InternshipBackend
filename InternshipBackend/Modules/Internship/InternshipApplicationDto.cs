namespace InternshipBackend.Modules.Internship;

public class InternshipApplicationDto
{
    public int Id { get; set; }
    public int InternshipPostingId { get; set; }
    public string? Message { get; set; }
    public string? CvUrl { get; set; }
}