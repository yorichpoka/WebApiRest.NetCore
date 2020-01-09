using System;

namespace WebApiRest.NetCore.Domain.Models
{
    public class TokenModel
    {
        public string Value { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public UserRoleModel User { get; set; }

        public TokenModel(string token, DateTime expirationDate)
        {
            this.Value = token;
            this.CreationDate = DateTime.Now;
            this.ExpirationDate = expirationDate;
        }
    }
}