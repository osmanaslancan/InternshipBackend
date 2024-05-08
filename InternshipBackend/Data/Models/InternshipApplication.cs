using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class InternshipApplication : UserOwnedEntity
{
    public int InternshipPostingId { get; set; }
    public string? Message { get; set; }
    public string? CvUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}