namespace InternshipBackend.Modules.UserProject;

public class UserProjectModifyDto
{
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public required string ProjectThumbnail { get; set; }
    public string? ProjectLink { get; set; }
}
