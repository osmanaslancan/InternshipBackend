using FluentValidation;

namespace InternshipBackend.Modules.Account;

public class UserInfoUpdateDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }
}

public class UserInfoUpdateDtoValidator : AbstractValidator<UserInfoUpdateDto>
{
    public UserInfoUpdateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
    }
}