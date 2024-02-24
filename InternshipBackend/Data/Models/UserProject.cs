namespace InternshipBackend.Data;

public class UserProject
{
    public int UserProjectId { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public required string ProjectThumbnail { get; set; }
    public string? ProjectLink { get; set; }
}
