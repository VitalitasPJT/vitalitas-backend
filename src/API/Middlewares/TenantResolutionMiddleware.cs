using Microsoft.AspNetCore.Http;

namespace API.Middlewares
{
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantResolutionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var claim = context.User.FindFirst("TenantId")?.Value;
                if (string.IsNullOrWhiteSpace(claim) || !Guid.TryParse(claim, out _))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new { message = "Token não contém identificação de tenant válida." });
                    return;
                }
            }

            await _next(context);
        }
    }
}
