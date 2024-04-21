
using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class Company : Entity
{
    public User? AdminUser { get; set; }
    public bool IsVertified { get; set; }
    public required string Name { get; set; }
    public int NumberOfWorkers { get; set; }
    public required string Description { get; set; }
    public ICollection<CompanyEmployee> Employees { get; set; } = [];
}
