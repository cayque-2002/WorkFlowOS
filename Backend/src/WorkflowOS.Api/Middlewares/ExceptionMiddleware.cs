using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WorkflowOS.Api.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problem = new ValidationProblemDetails(
                ex.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray()))
            {
                Title = "Erro de validação",
                Status = StatusCodes.Status400BadRequest
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = new ProblemDetails
            {
                Title = "Erro interno.",
                Detail = "Ocorreu um erro inesperado.",
                Status = StatusCodes.Status500InternalServerError
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}