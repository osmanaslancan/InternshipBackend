namespace InternshipBackend.Data;

public class CompanyEmployee
{
    public int UserId { get; set; }
    public required User User { get; set; }
    public int CompanyId { get; set; }
    public required Company Company { get; set; }
}
