using FluentValidation;

namespace InternshipBackend.Modules.Account;

public class UserInfoUpdateDtoValidator : AbstractValidator<UserInfoUpdateDto>
{
    public UserInfoUpdateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleSet("Create", () =>
        {
            RuleFor(x => x.AccountType).NotNull();
        });
    }
}