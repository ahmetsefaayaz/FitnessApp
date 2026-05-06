using System.Net;
using System.Text.Json;
using FitnessApp.Shared.Kernel.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FitnessApp.Shared.Kernel.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var message = "Sunucu kaynakli bir sorun olustu, lutfen yetkili birisine bildirin";
        var statusCode = (int)HttpStatusCode.InternalServerError;
        switch (exception)
        {
            case(NotFoundException):
                message = exception.Message;
                statusCode = (int)HttpStatusCode.NotFound;
                _logger.LogWarning(message);
                break;
            case(WrongInputException):
                message = exception.Message;
                statusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogWarning(message);
                break;
            case OperationCanceledException:
                _logger.LogInformation("User Disconnected");
                break;
            case UnauthorizedAccessException:
                message = exception.Message;
                statusCode = (int)HttpStatusCode.Unauthorized;
                _logger.LogWarning(message);
                break;
            default:
                _logger.LogError(exception, exception.Message);
                break;
        }

        if (context.Response.HasStarted)
        {
            return Task.CompletedTask;
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        var response = new
        {
            StatusCode = statusCode,
            Message = message
        };
        var jsonResponse = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonResponse);
    }
    
    
    
}