using ECommerceDemo.Application.Common;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace ECommerceDemo.API.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        int statusCode;
        object result;

        switch (exception)
        {
            case ValidationException validationException:

                statusCode = (int)HttpStatusCode.BadRequest;
                var errors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
                result = Result<string>.Failure(errors, HttpStatusCode.BadRequest);
                _logger.LogWarning(exception, "Validation hatası");
                break;

            case UnauthorizedAccessException:

                statusCode = (int)HttpStatusCode.Unauthorized;
                result = Result<string>.Failure("Yetkiniz yok.", HttpStatusCode.Unauthorized);
                _logger.LogWarning(exception, "Yetkisiz erişim");
                break;

            case DbUpdateException:

                statusCode = (int)HttpStatusCode.InternalServerError;
                result = Result<string>.Failure("Veritabanı hatası oluştu.", HttpStatusCode.InternalServerError);
                _logger.LogError(exception, "Veritabanı hatası");
                break;

            default:

                statusCode = (int)HttpStatusCode.InternalServerError;
                var message = _env.IsDevelopment() ? exception.Message : "Beklenmeyen bir hata oluştu.";
                result = Result<string>.Failure(message, HttpStatusCode.InternalServerError);
                _logger.LogError(exception, "Bilinmeyen hata");
                break;
        }

        httpContext.Response.StatusCode = statusCode;
        var json = JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        await httpContext.Response.WriteAsync(json, cancellationToken);
        return true;
    }
}
