using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WorkZen.Api.Infrastructure.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Unhandled exception caught by ApiExceptionFilter");

        var problemDetails = new ProblemDetails
        {
            Title = "Erro interno no servidor",
            Status = StatusCodes.Status500InternalServerError,
            Detail = context.Exception.Message,
            Instance = context.HttpContext.Request.Path
        };

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}