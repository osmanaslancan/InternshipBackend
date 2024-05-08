using FluentValidation;

namespace InternshipBackend.Modules.Account;

public class UserInfoUpdateDtoValidator : AbstractValidator<UserInfoUpdateDto>
{
    public UserInfoUpdateDtoValidator()
    {
        RuleSet("Update", () =>
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
        });
        RuleSet("Create", () =>
        {
            RuleFor(x => x.AccountType).NotNull();
        });
    }
}