namespace InternshipBackend.Modules.Internship;

public class InternshipApplicationCompanyDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int InternshipPostingId { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string ProfilePhotoUrl { get; set; }
    public string? Message { get; set; }
    public string? CvUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}