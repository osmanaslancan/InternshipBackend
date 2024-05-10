using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;

namespace InternshipBackend.Core;

public partial class ExceptionFilter(IStringLocalizer<ErrorCodeResource> stringLocalizer) : IExceptionFilter
{
    private static string ToSnakeCase(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        
        if (text.Length < 2) {
            return text.ToLowerInvariant();
        }
        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));
        for(var i = 1; i < text.Length; ++i) {
            var c = text[i];
            if(char.IsUpper(c)) {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            } else {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
    
    private static string RemovePropertyName(string errorMessage)
    {
        var regex = PropertyFinderRegex();
        return regex.Replace(errorMessage, "");
    }
    
    public void OnException(ExceptionContext context)
    {
        var response = new ServiceResponse
        {
            Error = new ServiceError
            {
                Name = "UnknownError",
                Details = context.Exception.Message
            }
        };

        if (context.Exception is ValidationException ve)
        {
            response.Error.Name = ve.Message;
            response.Error.Details = stringLocalizer[ve.Message];
            response.Error.Errors = ve.Errors.DistinctBy(e => e.PropertyName).ToDictionary(e => ToSnakeCase(e.PropertyName), e => RemovePropertyName(e.ErrorMessage));
        }

        if (context.Exception is PermissionException)
        {
            response.Error.Name = ErrorCodes.InsufficientPermission;
            response.Error.Details = stringLocalizer[ErrorCodes.InsufficientPermission];
        }
        else
        {
            context.Result = new BadRequestObjectResult(response);
        }


        context.ExceptionHandled = true;
    }

    [GeneratedRegex("^'.*?', ")]
    private static partial Regex PropertyFinderRegex();
}
