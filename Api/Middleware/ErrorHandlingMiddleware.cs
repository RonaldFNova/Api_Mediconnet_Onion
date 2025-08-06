using System.Text.Json;
using System.Net;

namespace Api_Mediconnet.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _requestDelegate;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ErrorHandlingMiddleware> logger)
    {
        _requestDelegate = requestDelegate;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContent)
    {
        try
        {
            await _requestDelegate(httpContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Se produjo un error no controlado.");
            await HandleExceptionAsync(httpContent, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)code;

        var errorResonse = new
        {
            status = (int)code,
            title = "Error",
            message = ex.Message
        };

        var json = JsonSerializer.Serialize(errorResonse);
        await httpContext.Response.WriteAsync(json);
    }
}
