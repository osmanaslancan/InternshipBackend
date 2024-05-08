using FluentValidation;
using InternshipBackend.Modules.App;

namespace InternshipBackend.Modules.UserProjects;

public class UserProjectModifyDtoValidator : AbstractValidator<UserProjectModifyDto>
{
    public UserProjectModifyDtoValidator()
    {
        RuleFor(x => x.ProjectName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
    }
}