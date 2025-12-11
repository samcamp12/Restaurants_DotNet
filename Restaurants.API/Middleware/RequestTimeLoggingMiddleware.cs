using System.Diagnostics;

namespace Restaurants.API.Middleware;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();
        await next(context);
        stopwatch.Stop();
        var elapsedTime = stopwatch.ElapsedMilliseconds;
        if (elapsedTime/1000 > 4)
        {
            logger.LogWarning("Request[{Verb}] to {RequestPath} took {ElapsedTime} ms.", context.Request.Method, context.Request.Path, elapsedTime);
        }
    }
}
