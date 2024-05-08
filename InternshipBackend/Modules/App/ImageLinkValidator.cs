using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Localization;

namespace InternshipBackend.Modules.App;

public class ImageLinkValidator<T>(IServiceProvider serviceProvider) : PropertyValidator<T, string?>
{
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (value is null)
        {
            return true;
        }
        
        var uploadImageService = serviceProvider.GetRequiredService<IUploadImageService>();
        return uploadImageService.IsOwnedByCurrentUser(value);
    }

    public override string Name => "ImageLinkValidator";
    
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        var stringLocalizer = serviceProvider.GetRequiredService<IStringLocalizer<ErrorCodeResource>>();
        return stringLocalizer[errorCode];
    }
}