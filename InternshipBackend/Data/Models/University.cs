using InternshipBackend.Core;

namespace InternshipBackend.Data;

public class University : IHasIdField
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
