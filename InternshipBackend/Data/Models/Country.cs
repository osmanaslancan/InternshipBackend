using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class Country : Entity
{
    public required string Name { get; set; }
    public string? Code { get; set; }
    public string? Code3 { get; set; }
    public string? PhoneCode { get; set; }
}
