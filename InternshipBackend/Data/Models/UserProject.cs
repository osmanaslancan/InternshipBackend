using InternshipBackend.Core;

namespace InternshipBackend.Data;

public class UserProject : IHasUserIdField, IHasIdField
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public required string ProjectThumbnail { get; set; }
    public string? ProjectLink { get; set; }
}
