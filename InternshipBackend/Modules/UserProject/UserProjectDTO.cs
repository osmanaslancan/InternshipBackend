using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(UserProject), ReverseMap = true)]
public class UserProjectDto
{
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public required string ProjectThumbnail { get; set; }
    public string? ProjectLink { get; set; }
}
