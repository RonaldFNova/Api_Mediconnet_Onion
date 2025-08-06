namespace Api_Mediconnet.Api.Middleware;

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder1)
    {
        return builder1.UseMiddleware<ErrorHandlingMiddleware>();
    }
}