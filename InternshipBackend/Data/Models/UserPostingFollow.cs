namespace InternshipBackend.Data.Models;

public class UserPostingFollow
{
    public int UserId { get; set; }
    public int PostingId { get; set; }
    public DateTime CreatedAt { get; set; }
}