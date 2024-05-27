namespace InternshipBackend.Data.Models;

public class UserCompanyFollow
{
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public DateTime CreatedAt { get; set; }
}