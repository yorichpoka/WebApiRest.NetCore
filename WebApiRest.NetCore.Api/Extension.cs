using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Api
{
    internal static class Extension
    {
        internal static void ExtAddVersionInHeaderResponse(this HttpResponse value, string appVersion)
        {
            value.Headers.Add("AppVersion", appVersion);
        }

        internal static string ExtToString(this HttpRequest value, Formatting formatting = Formatting.Indented)
        {
            return
                JsonConvert.SerializeObject(
                    new
                    {
                        ContentLength = value.ContentLength,
                        ContentType = value.ContentType,
                        Cookies = value.Cookies,
                        // Form = value.Form,
                        HasFormContentType = value.HasFormContentType,
                        Headers = value.Headers,
                        Host = value.Host,
                        IsHttps = value.IsHttps,
                        Method = value.Method,
                        Path = value.Path,
                        PathBase = value.PathBase,
                        Protocol = value.Protocol,
                        // Query = value.Query,
                        QueryString = value.QueryString,
                        Scheme = value.Scheme
                    },
                    formatting
                );
        }

        internal static TokenModel ExtGetJwt(this string value, TimeSpan duration, List<Claim> claims = null)
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
            var tokenValue = new TokenModel(
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