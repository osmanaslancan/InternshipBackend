using System.ComponentModel.DataAnnotations;
using InternshipBackend.Data.Models.Enums;
using InternshipBackend.Data.Models.ValueObjects;

namespace InternshipBackend.Modules.UserDetails;

public class UserDetailListDto
{
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public ICollection<DriverLicense> DriverLicenses { get; set; } = [];
    public List<UserCv> Cvs { get; set; } = [];
    public MaritalStatus? MaritalStatus { get; set; }
    public MilitaryStatus? MilitaryStatus { get; set; }
    public int? CountryId { get; set; }
    public int? CityId { get; set; }
    public string? District { get; set; }
    public string? Address { get; set; }
}
