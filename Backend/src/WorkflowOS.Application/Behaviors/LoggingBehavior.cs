using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace WorkflowOS.Application.Common.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestId = Guid.NewGuid();
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation(
            "[{RequestId}] Iniciando execução do comando {Command}. Payload: {@Request}",
            requestId,
            requestName,
            request);

        var stopwatch = Stopwatch.StartNew();

        try
        {
            var response = await next();

            stopwatch.Stop();

            _logger.LogInformation(
                "[{RequestId}] Comando {Command} executado com sucesso em {ElapsedMilliseconds}ms",
                requestId,
                requestName,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(
                ex,
                "[{RequestId}] Erro ao executar o comando {Command} após {ElapsedMilliseconds}ms",
                requestId,
                requestName,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }
}