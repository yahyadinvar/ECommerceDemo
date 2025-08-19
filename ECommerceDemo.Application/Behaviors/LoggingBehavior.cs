﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceDemo.Application.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Handling {RequestName} with data: {@Request}", requestName, request);

        try
        {
            var response = await next();

            _logger.LogInformation("Handled {RequestName} with response: {@Response}", requestName, response);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while handling {RequestName} with data: {@Request}", requestName, request);
            throw;  // Exception'ı yeniden fırlat, aksi halde hata yakalanmış gibi davranılır.
        }
    }
}

