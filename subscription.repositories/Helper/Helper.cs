
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using subscription.models.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace subscription.repositories.Helper
{
    public class Helpers : IHelper
    {
        private IConfiguration _configuration;

        public Helpers(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwt(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),

                new Claim("exp2", DateTime.Now.AddHours(12).ToString() )



            //new Claim("Jti", Guid.NewGuid().ToString()),
        };




            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuthentication:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtAuthentication:JwtIssuer"],
                audience: _configuration["JwtAuthentication:JwtAudience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
