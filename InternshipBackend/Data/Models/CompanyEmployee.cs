using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class CompanyEmployee : Entity
{
    public int UserId { get; set; }
    public required User User { get; set; }
    public int CompanyId { get; set; }
    public required Company Company { get; set; }
}
