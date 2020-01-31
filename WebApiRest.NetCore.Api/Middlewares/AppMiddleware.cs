using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRest.NetCore.Api.Middlewares
{
    public class AppMiddleware
    {
        private readonly RequestDelegate _Next;

        public AppMiddleware(RequestDelegate next)
        {
            this._Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            /*
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;

            }
            */

            // Call the next delegate/middleware in the pipeline
            await this._Next(context);
        }
    }
}
