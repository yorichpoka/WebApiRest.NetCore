using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace WebApiRest.NetCore.Api.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        private readonly IConfiguration _Configuration;

        public CustomActionFilter(IConfiguration confirugation)
        {
            this._Configuration = confirugation;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Log
            Log.Information(
                context.HttpContext.Request.ExtToString()
            );
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.ExtAddVersionInHeaderResponse(
                this._Configuration.GetSection("AppSettings:Version").Value
            );
        }
    }
}