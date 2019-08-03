using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace WebApiRest.NetCore.Filters
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
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.ExtAddVersionInHeaderResponse(
                this._Configuration.GetSection("AppSettings:Version").Value
            );
        }
    }
}