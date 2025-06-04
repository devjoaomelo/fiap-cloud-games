using System.Net;
using System.Text.Json;

namespace FCG.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IHostEnvironment env)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro: {ExceptionType} - {Message}", ex.GetType().Name, ex.Message);
            await HandleExceptionAsync(context, ex, env);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env)
    {
        var statusCode = ex switch
        {
            ArgumentException => HttpStatusCode.BadRequest,
            InvalidOperationException => HttpStatusCode.Conflict,
            KeyNotFoundException => HttpStatusCode.NotFound,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };

        var response = new
        {
            Status = (int)statusCode,
            Error = ex.GetType().Name,
            Message = ex.Message,
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}