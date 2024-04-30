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
        When(x => x.IsGraduated == true, () =>
        {
            RuleFor(x => x.EndDate).NotNull().GreaterThan(x => x.StartDate);
        }).Otherwise(() =>
        {
            RuleFor(x => x.EndDate).Null();
        });
        
        RuleFor(x => x.IsGraduated).NotNull();
        RuleFor(x => x.Description).MaximumLength(1000);
        RuleFor(x => x.GPA).InclusiveBetween(0, 4);
        
        When(x => string.IsNullOrEmpty(x.UniversityName), () =>
        {
            RuleFor(x => x.UniversityId).NotNull();
        }).Otherwise(() =>
        {
            RuleFor(x => x.UniversityId).Null();
        });
    }
}