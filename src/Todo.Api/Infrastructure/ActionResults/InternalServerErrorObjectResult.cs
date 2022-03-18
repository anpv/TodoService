using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Infrastructure.ActionResults;

public sealed class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object? value) : base(value)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}
