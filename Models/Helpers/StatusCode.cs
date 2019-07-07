﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApiRest.NetCore.Models.Helpers
{
    public class StatusCode
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public StatusCode(int statusCode)
        {
            this.Code = statusCode;
            this.Message = ((HttpStatusCode)statusCode).ToString();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
