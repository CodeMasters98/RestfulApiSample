namespace RestfulApiSample.Extentions
{

    public class UserAgentMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserAgentMiddleware> _logger;

        public UserAgentMiddleware(RequestDelegate requestDelegate, ILogger<UserAgentMiddleware> logger)
        {
            this._next = requestDelegate;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"];
            if (!string.IsNullOrEmpty(userAgent))
            {
                await this._next(context);
                _logger.LogInformation("Invalid User Agent");
            }
        }
    }

    public static class LogUserAgentExtention
    {
        public static IApplicationBuilder UseLogUserAgent(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UserAgentMiddleware>();
        }
    }
}

