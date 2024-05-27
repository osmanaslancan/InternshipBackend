namespace InternshipBackend.Modules.UserProjects;

public class UserProjectListDto : UserProjectModifyDto
{
    public int Id { get; set; }
    public string? ProjectThumbnail { get; set; }
}
