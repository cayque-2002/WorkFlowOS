using WorkflowOS.Api.Middlewares;

namespace WorkflowOS.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}