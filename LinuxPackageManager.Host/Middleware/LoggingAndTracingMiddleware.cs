using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace LinuxPackageManager.Host.Middleware;

public class LoggingAndTracingMiddleware
{
    private const string CorrelationIdHeaderName = "X-Request-ID";
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingAndTracingMiddleware> _logger;

    public LoggingAndTracingMiddleware(RequestDelegate next, ILogger<LoggingAndTracingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = GetOrGenerateCorrelationId(context);
        
        using (_logger.BeginScope("{@CorrelationId}", correlationId))
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("Request started: {Method} {Path}", 
                context.Request.Method, 
                context.Request.Path);

            await _next(context);

            stopwatch.Stop();
            _logger.LogInformation("Request finished: {Method} {Path} with status {StatusCode} in {ElapsedMilliseconds}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }

    private string GetOrGenerateCorrelationId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var correlationIdValues) &&
            correlationIdValues.FirstOrDefault() is { } existingId &&
            !string.IsNullOrEmpty(existingId))
        {
            context.Response.Headers.Append(CorrelationIdHeaderName, existingId);
            return existingId;
        }
        var newId = Guid.NewGuid().ToString();
        context.Response.Headers.Append(CorrelationIdHeaderName, newId);
        return newId;
    }
}