using System.Diagnostics;
using System.Security.Claims;
using api.DTOs.Analytics.ClickHouse;
using api.Repository.Analytics.interfaces;

namespace api.Middleware;

public class AnalyticsMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
{
    private static readonly HashSet<string> IgnoredPaths = new(StringComparer.OrdinalIgnoreCase)
    {
        "/metrics", "/health", "/health/ready", "/health/live", "/favicon.ico"
    };

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        await next(context);
        sw.Stop();

        var path = context.Request.Path.Value ?? "";
        if (IgnoredPaths.Contains(path)) return;

        // Fire-and-forget — analytics must never slow down or break the response
        _ = TrackAsync(context, path, sw.Elapsed.TotalMilliseconds);
    }

    private async Task TrackAsync(HttpContext context, string path, double durationMs)
    {
        try
        {
            using var scope = scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IClickHouseRepository>();

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? userId = Guid.TryParse(userIdClaim, out var uid) ? uid : null;

            await repo.TrackApiRequestAsync(new ApiRequestRecord(
                Endpoint: path,
                Method: context.Request.Method,
                StatusCode: context.Response.StatusCode,
                DurationMs: durationMs,
                UserId: userId));
        }
        catch
        {
            // Swallow — analytics failure must not surface to the caller
        }
    }
}
