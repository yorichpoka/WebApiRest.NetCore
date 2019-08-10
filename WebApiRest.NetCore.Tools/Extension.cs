using Microsoft.AspNetCore.Http;
using System.Net;

namespace WebApiRest.NetCore.Tools
{
    public static class Extension
    {
        public static int ExtConvertToInt32(this string value)
        {
            return
              int.Parse(value);
        }

        public static void ExtAddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int ExtConvertToInt32(this HttpStatusCode value)
        {
            return (int)value;
        }
    }
}