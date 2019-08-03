using Microsoft.AspNetCore.Http;

namespace WebApiRest.NetCore
{
    public static class Extension
    {
        public static void ExtAddVersionInHeaderResponse(this HttpResponse value, string appVersion)
        {
            value.Headers.Add("AppVersion", appVersion);
        }
    }
}