using FluentValidation;

namespace InternshipBackend.Modules.Internship;

public class InternshipApplicationDtoValidator : AbstractValidator<InternshipApplicationDto>
{
    public InternshipApplicationDtoValidator()
    {
        RuleFor(x => x.InternshipPostingId).NotNull();
        RuleFor(x => x.Message).MaximumLength(1000);
        RuleFor(x => x.CvUrl).MaximumLength(100);
    }
}