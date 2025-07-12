using System.Globalization;

namespace Blog.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower() ?? "/";
            var authorization = context.Request.Cookies["Authorization"];

            // Routes that don't require authentication
            var allowURI = new List<string>
            {
                "/",
                "/home/privacy",
                "/auth/login",
                "/auth/register"
            };

            // If the route is allowed without auth
            if (allowURI.Contains(path))
            {
                // Only redirect authenticated users away from login/register
                if (!string.IsNullOrEmpty(authorization) && (path == "/auth/login" || path == "/auth/register"))
                {
                    context.Response.Redirect("/");
                    return;
                }

                await _next(context);
                return;
            }

            // All other routes require authorization
            if (string.IsNullOrEmpty(authorization))
            {
                context.Response.Redirect("/auth/login");
                return;
            }

            await _next(context);
        }
    }
}
