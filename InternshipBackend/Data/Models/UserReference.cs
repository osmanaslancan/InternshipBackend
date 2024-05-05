using System.ComponentModel.DataAnnotations;
using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class UserReference : UserOwnedEntity
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Company { get; set; }
    public string? Duty { get; set; }
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }
}