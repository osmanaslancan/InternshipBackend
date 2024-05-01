using InternshipBackend.Data.Models.Enums;

namespace InternshipBackend.Modules.Account;

public class UserInfoUpdateDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }

    public AccountType? AccountType { get; set; }
}