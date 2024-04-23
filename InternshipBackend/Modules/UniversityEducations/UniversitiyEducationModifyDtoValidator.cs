using FluentValidation;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.UniversityEducations;

public class UniversitiyEducationModifyDtoValidator : AbstractValidator<UniversityEducationModifyDto>
{
    public UniversitiyEducationModifyDtoValidator()
    {
        RuleFor(x => x.Department).NotNull();
        RuleFor(x => x.EducationYear).NotNull().LessThanOrEqualTo(6);
        RuleFor(x => x.StartDate).NotNull();
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
        RuleFor(x => x.IsGraduated).NotNull();
        RuleFor(x => x.Description).MaximumLength(1000);
        RuleFor(x => x.GPA).InclusiveBetween(0, 4);
    }
}