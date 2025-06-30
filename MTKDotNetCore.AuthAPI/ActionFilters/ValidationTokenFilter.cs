using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MTKDotNetCore.AuthAPI.Helpers;
using MTKDotNetCore.AuthAPI.ResponseModels;
using Newtonsoft.Json;

namespace MTKDotNetCore.AuthAPI.ActionFilters
{
    public class ValidationTokenFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            EncDecHelper _encDecHelper = context.HttpContext.RequestServices.GetService<EncDecHelper>()!;

            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var result);

            if (result.Count < 0)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            ResponseUser user = JsonConvert.DeserializeObject<ResponseUser>(_encDecHelper!.DecString(result!))!;

            if (user.SessionExpired < DateTime.Now)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
            // Do something after the action executes.
        }
    }
}
