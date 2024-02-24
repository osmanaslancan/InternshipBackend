using FluentValidation;

namespace InternshipBackend.Modules;

public class CreateAccountDTOValidator : AbstractValidator<CreateAccountDTO>
{
    public CreateAccountDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
    }
}


public class UserInfoUpdateDTOValidator : AbstractValidator<UserInfoUpdateDTO>
{
    public UserInfoUpdateDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Age).GreaterThan(0);
    }
}
