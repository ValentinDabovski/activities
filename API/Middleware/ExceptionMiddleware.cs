using System.Net;
using System.Text.Json;

namespace API.Middleware;

public class ExceptionMiddleware
{
    private readonly IHostEnvironment env;
    private readonly ILogger<ExceptionMiddleware> logger;
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        this.env = env;
        this.logger = logger;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                ? new ApplicationException(context.Response.StatusCode, e.Message, e.StackTrace)
                : new ApplicationException(context.Response.StatusCode, "Internal Server Error");

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}

internal class ApplicationException
{
    public ApplicationException(int statusCode, string message, string details = default)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;
    }

    private int StatusCode { get; }

    private string Message { get; }

    private string Details { get; }
}