namespace E_Commerce.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"Request Path: {context.Request.Path}");
        _logger.LogInformation($"Request Method: {context.Request.Method}");

        await _next(context); // Pass control to the next middleware

        _logger.LogInformation($"{context.Request.Path} completed");
    }
}
