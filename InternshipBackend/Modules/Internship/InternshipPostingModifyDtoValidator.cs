using FluentValidation;
using InternshipBackend.Modules.App;

namespace InternshipBackend.Modules.Internship;

public class InternshipPostingModifyDtoValidator : AbstractValidator<InternshipPostingModifyDto>
{
    public InternshipPostingModifyDtoValidator(IServiceProvider serviceProvider)
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.ImageUrl).OwnedByCurrentUser(serviceProvider);
        RuleFor(x => x.BackgroundPhotoUrl).OwnedByCurrentUser(serviceProvider);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Sector).MaximumLength(255);
        RuleFor(x => x.DeadLine).NotEmpty();
    }
}
