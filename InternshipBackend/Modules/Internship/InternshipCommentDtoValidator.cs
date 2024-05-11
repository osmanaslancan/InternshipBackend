using FluentValidation;

namespace InternshipBackend.Modules.Internship;

public class InternshipCommentDtoValidator : AbstractValidator<InternshipCommentDto>
{
    public InternshipCommentDtoValidator()
    {
        RuleFor(x => x.InternshipPostingId).NotNull();
        RuleFor(x => x.Comment).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Points).InclusiveBetween(0, 10);
        
    }
}