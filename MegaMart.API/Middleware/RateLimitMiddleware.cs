using MegaMart.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace MegaMart.API.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);
        private readonly int _maxRequests = 8;

        public RateLimitMiddleware(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            ApplySecurity(context);

            if (!IsRequestAllowed(context))
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(
                    new ApiExceptions(context.Response.StatusCode, "Too Many Requests. Please try again later"));
                return; // Stop further processing
            }

            await _next(context);

        }

        private bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ip)) return true;

            var cacheKey = $"RateLimit:{ip}";
            var now = DateTime.UtcNow;
            var success = _memoryCache.TryGetValue(cacheKey, out RateLimitEntry entry);

            // Check if entry exists and is still valid
            if (success && entry.Expiration > now)
            {
                if (entry.Count >= _maxRequests)
                    return false;

                // Atomic increment to ensure thread safety
                Interlocked.Increment(ref entry.Count);
                return true;
            }

            // Create new entry or reset expired entry
            var newEntry = new RateLimitEntry
            {
                Count = 1,
                Expiration = now.Add(_rateLimitWindow)
            };

            _memoryCache.Set(cacheKey, newEntry, newEntry.Expiration);
            return true;
        }

        private class RateLimitEntry
        {
            public int Count;
            public DateTime Expiration;
        }

        private void ApplySecurity(HttpContext httpContext)
        {
            httpContext.Response.Headers["C-Content-Type_Options"] = "nosniff";

            httpContext.Response.Headers["X-XSS-Prodection"] = "1:mode=bloock";

            httpContext.Response.Headers["X-Frame-Options"] = "DENY";
        }


    }
}
