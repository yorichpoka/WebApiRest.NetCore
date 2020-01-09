using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiRest.NetCore.Service
{
    public static class Extension
    {
        public static int ExtConvertTo(this string value)
        {
            return int.Parse(value);
        }
    }
}
