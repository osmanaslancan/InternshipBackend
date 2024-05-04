using FluentValidation;

namespace InternshipBackend.Modules.WorkHistory;

public class WorkHistoryModifyDtoValidator : AbstractValidator<WorkHistoryModifyDto>
{
    public WorkHistoryModifyDtoValidator()
    {
        RuleFor(x => x.Position).NotEmpty();
        RuleFor(x => x.CompanyName).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        When(x => x.IsWorkingNow, () =>
        {
            RuleFor(x => x.EndDate).NotEmpty();
        });
        RuleFor(x => x.IsWorkingNow).NotNull();
        RuleFor(x => x.Description).MaximumLength(1000);
        RuleFor(x => x.Duties).MaximumLength(255);
        RuleFor(x => x.WorkType).NotNull();
        RuleFor(x => x.ReasonForLeave).MaximumLength(255);
    }
}