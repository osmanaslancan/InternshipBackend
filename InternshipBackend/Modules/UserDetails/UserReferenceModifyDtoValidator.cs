using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace InternshipBackend.Modules.UserDetails;

public class UserReferenceModifyDtoValidator : AbstractValidator<UserReferenceModifyDto>
{
    public UserReferenceModifyDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Company).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Duty).MaximumLength(255);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.PhoneNumber).MaximumLength(255);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}
