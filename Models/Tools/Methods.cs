using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiRest.NetCore.Models.Helpers;

namespace WebApiRest.NetCore.Models
{
    public static class Methods
    {
        public static TokenJWT GetJWT(string securityKey, TimeSpan duration, List<Claim> claims = null)
        {
            // Default value.
            var expirationDate = DateTime.Now.Add(duration);

            // Validation.
            if (expirationDate < DateTime.Now)
                throw new ArgumentException("Value must not be negative", "duration", null);

            // Symmetric security key
            var symmetricSecurityKey = GetSymmetricSecurityKey(securityKey);

            // Signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            // Create token
            var token = new JwtSecurityToken(
                            issuer: "projects.in",
                            audience: "readers",
                            expires: expirationDate,
                            signingCredentials: signingCredentials,
                            claims: claims
                        );

            // token
            var tokenValue = new TokenJWT(
                                    new JwtSecurityTokenHandler().WriteToken(token),
                                    expirationDate
                                );

            // Return
            return tokenValue;
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            // Symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            return symmetricSecurityKey;
        }
    }
}
