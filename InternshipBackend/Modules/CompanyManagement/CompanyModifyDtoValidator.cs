using FluentValidation;
using InternshipBackend.Data.Models.Enums;
using InternshipBackend.Modules.App;

namespace InternshipBackend.Modules.CompanyManagement;

public class CompanyModifyDtoValidator : AbstractValidator<CompanyModifyDto>
{
    public CompanyModifyDtoValidator(IServiceProvider serviceProvider)
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        RuleFor(x => x.ShortDescription).MaximumLength(75);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.LogoUrl).OwnedByCurrentUser(serviceProvider);
        RuleFor(x => x.BackgroundPhotoUrl).OwnedByCurrentUser(serviceProvider);
    }
}
