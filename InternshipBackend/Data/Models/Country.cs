using InternshipBackend.Core;

namespace InternshipBackend.Data;

public class Country : IHasIdField
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Code { get; set; }
    public string? Code3 { get; set; }
    public string? PhoneCode { get; set; }
}
