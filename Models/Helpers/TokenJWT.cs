using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRest.NetCore.Models.Helpers
{
    public class TokenJWT
    {
        public string value { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime expirationDate { get; set; }

        public TokenJWT(string token, DateTime expirationDate)
        {
            this.value = token;
            this.creationDate = DateTime.Now;
            this.expirationDate = expirationDate;
        }
    }
}
