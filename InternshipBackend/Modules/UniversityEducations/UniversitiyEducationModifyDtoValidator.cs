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
    }
}