using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace API.Middleware;

public class ExceptionMiddleware
{
    private readonly IHostEnvironment _env;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _env = env;
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (e is BaseDomainException domainException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = new ApplicationException(context.Response.StatusCode, domainException.Error);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
            else
            {
                var response = new ApplicationException(context.Response.StatusCode, e.Message);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}

internal class ApplicationException(int statusCode, string message)
{
    public int StatusCode { get; private set; } = statusCode;

    public string Message { get; private set; } = message;
}