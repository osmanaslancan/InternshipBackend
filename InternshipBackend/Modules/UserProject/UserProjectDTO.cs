using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(UserProject))]
public class UserProjectDTO
{
    public int Id { get; set; }
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public required string ProjectThumbnail { get; set; }
    public string? ProjectLink { get; set; }
}
