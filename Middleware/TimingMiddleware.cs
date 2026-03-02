using System.Diagnostics;

namespace MiniService.Middleware;

public class TimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TimingMiddleware> _logger;

    public TimingMiddleware(
        RequestDelegate next,
        ILogger<TimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        _logger.LogInformation(
            "Request {RequestId} executed in {ElapsedMilliseconds} ms",
            context.TraceIdentifier,
            stopwatch.ElapsedMilliseconds);
    }
}