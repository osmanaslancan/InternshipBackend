using FluentValidation;

namespace InternshipBackend.Modules.App;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string?> OwnedByCurrentUser<T>(this IRuleBuilder<T, string?> ruleBuilder, IServiceProvider serviceProvider)
    {
        return ruleBuilder.SetValidator(new ImageLinkValidator<T>(serviceProvider));
    }
}