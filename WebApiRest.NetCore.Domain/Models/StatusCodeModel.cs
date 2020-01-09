using Newtonsoft.Json;
using System.Net;

namespace WebApiRest.NetCore.Domain.Models
{
    public class StatusCodeModel
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public StatusCodeModel(int statusCode)
        {
            this.Code = statusCode;
            this.Message = statusCode != 401 ? ((HttpStatusCode)statusCode).ToString()
                                             : @"You must connect to ""api/users/auth"" with the ""login"" and ""password"" parameters.";
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}