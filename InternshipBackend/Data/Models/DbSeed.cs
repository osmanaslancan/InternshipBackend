using System.ComponentModel.DataAnnotations;

namespace InternshipBackend.Data;

public class DbSeed
{
    [Key]
    public required string SeederName { get; set; }
    public DateTime AppliedAt { get; set; }
}
