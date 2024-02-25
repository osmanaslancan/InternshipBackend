using System.ComponentModel.DataAnnotations;

namespace InternshipBackend.Data;

public class UserDetail
{
    [Key]
    public int UserId { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public ICollection<string> DriverLicences { get; set; } = [];
    public MaritalStatus MaritalStatus { get; set; }
    public MilitaryStatus MilitaryStatus { get; set; }
    public Country? Country { get; set; }
    public City? City { get; set; }
    public string? District { get; set; }
    public string? Address { get; set; }
}
