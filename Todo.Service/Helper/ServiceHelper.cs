using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;

namespace Todo.Service
{
    public class ServiceHelper
    {
        internal static async Task<string> JwtTokenProvider(TodoUser user, List<Claim> roles)
        {

            var claims = new List<Claim>
           {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            claims.AddRange(roles);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(60);

            var token = new JwtSecurityToken(
                Environment.GetEnvironmentVariable("JwtIssuer"),
                Environment.GetEnvironmentVariable("JwtAudience"),
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        internal static DateTime StringToDateTime(string date)
        {
            string dateFormat = "yyyy-MM-ddTHH:mm";
            return DateTime.ParseExact(date, dateFormat, null);
        }
    }
}
