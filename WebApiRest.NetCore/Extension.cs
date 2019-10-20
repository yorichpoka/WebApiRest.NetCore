using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore
{
    internal static class Extension
    {
        internal static void ExtAddVersionInHeaderResponse(this HttpResponse value, string appVersion)
        {
            value.Headers.Add("AppVersion", appVersion);
        }
        
        internal static TokenJwtModel ExtGetJwt(this string value, TimeSpan duration, List<Claim> claims = null)
        {
            // Default value.
            var expirationDate = DateTime.Now.Add(duration);

            // Validation.
            if (expirationDate < DateTime.Now)
                throw new ArgumentException("Value must not be negative", "duration", null);

            // Symmetric security key
            var symmetricSecurityKey = ExtGetSymmetricSecurityKey(value);

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
            var tokenValue = new TokenJwtModel(
                                    new JwtSecurityTokenHandler().WriteToken(token),
                                    expirationDate
                                );

            // Return
            return tokenValue;
        }

        internal static SymmetricSecurityKey ExtGetSymmetricSecurityKey(this string securityKey)
        {
            // Symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            return symmetricSecurityKey;
        }
    }
}