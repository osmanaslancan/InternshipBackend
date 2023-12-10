using FluentValidation;

namespace InternshipBackend.Modules;

public class UserInfoDTOValidator : AbstractValidator<CreateUserInfoDTO>
{
    public UserInfoDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Age).InclusiveBetween(18, 100);
        RuleFor(x => x.UniversityId);
    }
}
