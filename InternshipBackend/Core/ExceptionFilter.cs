using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InternshipBackend.Core;

public class ExceptionFilter : IExceptionFilter
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
            response.Error.Name = "ValidationError";
            response.Error.Errors = ve.Errors.DistinctBy(e => e.PropertyName).ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
        }

        context.Result = new BadRequestObjectResult(response);

        context.ExceptionHandled = true;
    }
}
