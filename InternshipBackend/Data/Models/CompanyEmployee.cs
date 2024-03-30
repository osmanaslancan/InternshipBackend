using InternshipBackend.Core;

namespace InternshipBackend.Data;

public class CompanyEmployee : IHasIdField
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public int CompanyId { get; set; }
    public required Company Company { get; set; }
}
