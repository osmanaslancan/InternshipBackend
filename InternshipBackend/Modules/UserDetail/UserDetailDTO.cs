using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(UserDetail), ReverseMap = true)]
public class UserDetailDTO
{
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public ICollection<string> DriverLicences { get; set; } = [];
    public MaritalStatus MaritalStatus { get; set; }
    public MilitaryStatus MilitaryStatus { get; set; }
    public int? CountryId { get; set; }
    public int? CityId { get; set; }
    public string? District { get; set; }
    public string? Address { get; set; }
}
