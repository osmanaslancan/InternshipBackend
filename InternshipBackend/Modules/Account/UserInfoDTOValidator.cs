using FluentValidation;

namespace InternshipBackend.Modules;

public class UserInfoDTOValidator : AbstractValidator<CreateAccountDTO>
{
    public UserInfoDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
