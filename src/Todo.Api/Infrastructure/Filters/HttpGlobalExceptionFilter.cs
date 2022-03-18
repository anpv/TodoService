using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Todo.Api.Infrastructure.ActionResults;
using Todo.App.Exceptions;
using Todo.Domain.Exceptions;

namespace Todo.Api.Infrastructure.Filters;

public sealed class HttpGlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<HttpGlobalExceptionFilter> logger;

    public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger) => this.logger = logger;

    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "Global unhandled exception.");
        if (context.Exception is TodoDomainException domainException)
        {
            HandleDomainException(context, domainException);
        }
        else
        {
            context.Result = new InternalServerErrorObjectResult(
                new
                {
                    Messages = new[] { "An error occured. Try it again." }
                });
        }

        context.ExceptionHandled = true;
    }

    private static void HandleDomainException(ExceptionContext context, TodoDomainException exception)
    {
        var problemDetails = new ValidationProblemDetails
        {
            Detail = "Please refer to the errors property for additional details.",
            Instance = context.HttpContext.Request.Path,
            Errors =
            {
                ["DomainValidations"] = new[] { exception.Message }
            }
        };

        context.Result = exception switch
        {
            TodoItemNotFoundException => new NotFoundObjectResult(problemDetails),
            _ => new BadRequestObjectResult(problemDetails)
        };
    }
}
