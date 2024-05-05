using FluentValidation;

namespace InternshipBackend.Modules.UserProjects;

public class UserProjectModifyDtoValidator : AbstractValidator<UserProjectModifyDto>
{
    public UserProjectModifyDtoValidator()
    {
        RuleFor(x => x.ProjectName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.ProjectThumbnail).MaximumLength(255);
    }
}