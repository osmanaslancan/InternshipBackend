
namespace InternshipBackend.Data;

public class Company
{
    public int CompanyId { get; set; }
    public User? AdminUser { get; set; }
    public bool IsVertified { get; set; }
    public required string Name { get; set; }
    public int NumberOfWorkers { get; set; }
    public required string Description { get; set; }
}
