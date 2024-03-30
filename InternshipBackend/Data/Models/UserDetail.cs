﻿using InternshipBackend.Core;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Data;

public class UserDetail : IHasIdField, IHasUserIdField
{
    public int Id { get; set; }
    public required User User { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public ICollection<DriverLicense> DriverLicences { get; set; } = [];
    public MaritalStatus MaritalStatus { get; set; }
    public MilitaryStatus MilitaryStatus { get; set; }
    public Country? Country { get; set; }
    public int? CountryId { get; set; }
    public City? City { get; set; }
    public int? CityId { get; set; }
    public string? District { get; set; }
    public string? Address { get; set; }

    int IHasUserIdField.UserId { get => Id; set => Id = value; }
}
