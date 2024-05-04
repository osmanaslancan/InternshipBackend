using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InternshipBackend.Modules.UserProject;

public class UserProjectModifyDtoValidator : AbstractValidator<UserProjectModifyDto>
{
    public UserProjectModifyDtoValidator()
    {
        RuleFor(x => x.ProjectName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.ProjectThumbnail).MaximumLength(255);
        RuleFor(x => x.ProjectLink).MaximumLength(255);
    }
}