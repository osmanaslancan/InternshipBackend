using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;

namespace InternshipBackend.Core;

public class ExceptionFilter(IStringLocalizer<ErrorCodeResource> stringLocalizer) : IExceptionFilter
{
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
            response.Error.Errors = ve.Errors.DistinctBy(e => e.PropertyName).ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
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
}
