using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApiRest.NetCore.Models
{
  public static class Methods
  {
    public static string GetJWT(string securityKey, List<Claim> claims = null)
    {
      // Symmetric security key
      var symmetricSecurityKey = GetSymmetricSecurityKey(securityKey);

      // Signing credentials
      var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

      // Create token
      var token = new JwtSecurityToken(
                      issuer: "projects.in",
                      audience: "readers",
                      expires: DateTime.Now.AddMinutes(5),
                      signingCredentials: signingCredentials,
                      claims: claims
                  );

      // token
      var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

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
