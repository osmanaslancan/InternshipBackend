using System.ComponentModel.DataAnnotations;
using InternshipBackend.Core;
using InternshipBackend.Data.Models.Enums;
using InternshipBackend.Data.Models.ValueObjects;

namespace InternshipBackend.Data.Models;

public class UserDetail : Entity, IHasUserIdField
{
    public required User User { get; set; }
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public ICollection<DriverLicense> DriverLicences { get; set; } = [];
    public MaritalStatus? MaritalStatus { get; set; }
    public MilitaryStatus? MilitaryStatus { get; set; }
    public Country? Country { get; set; }
    public int? CountryId { get; set; }
    public City? City { get; set; }
    public int? CityId { get; set; }
    public string? District { get; set; }
    public string? Address { get; set; }
    public UserDetailExtras? Extras { get; set; }

    int IHasUserIdField.UserId { get => Id; set => Id = value; }
}
