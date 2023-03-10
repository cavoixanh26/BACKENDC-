using Microsoft.IdentityModel.Tokens;
 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManagement.Models;

namespace SWP_Login.Utils
{
    public class GenerateToken
    {
        public static string GenerateMyToken(User acc)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json")
                          .Build();
            var key = 
                new SymmetricSecurityKey
                (System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name ,acc.UserName),
                new Claim(ClaimTypes.Role ,"Basic")
            };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims
                , expires: DateTime.UtcNow.AddHours(7.05)
                , signingCredentials: creds
                ) ;
             
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
