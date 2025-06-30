using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.AuthAPI.Helpers;
using MTKDotNetCore.AuthAPI.ResponseModels;
using Newtonsoft.Json;
using System.Globalization;
using System.Security;

namespace MTKDotNetCore.AuthAPI.Middlewares
{
    public class ValidationTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            string currentPath = context.Request.Path.ToString().ToLower();
            List<string> paths = new List<string> { "/weatherforecast", "/api/login" };
            if (paths.Contains(currentPath))
            {
                goto Result;
            }

            EncDecHelper _encDecHelper = context.RequestServices.GetService<EncDecHelper>()!;

            context.Request.Headers.TryGetValue("Authorization", out var result);

            if (string.IsNullOrEmpty(result))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            ResponseUser user = JsonConvert.DeserializeObject<ResponseUser>(_encDecHelper!.DecString(result!))!;

            if (user.SessionExpired < DateTime.Now)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

        Result:
            await _next(context);
        }
    }

    public static class ValidationTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidationTokenMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationTokenMiddleware>();
        }
    }
}
