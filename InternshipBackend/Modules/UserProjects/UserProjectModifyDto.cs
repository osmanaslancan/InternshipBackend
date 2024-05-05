namespace InternshipBackend.Modules.UserProjects;

public class UserProjectModifyDto
{
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public string? ProjectThumbnail { get; set; }
}
