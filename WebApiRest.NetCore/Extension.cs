using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRest.NetCore
{
    public static class Extension
    {
        public static void ExtAddVersionInHeaderResponse(this HttpResponse value, string appVersion)
        {
            value.Headers.Add( "AppVersion", appVersion);
        }
    }
}
