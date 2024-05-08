namespace InternshipBackend.Modules.UserProjects;

public class UserProjectModifyDto
{
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public string? ProjectLink { get; set; }
}
