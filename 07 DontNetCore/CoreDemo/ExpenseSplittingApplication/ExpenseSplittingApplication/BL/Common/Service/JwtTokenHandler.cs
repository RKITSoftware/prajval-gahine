using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseSplittingApplication.BL.Common.Service
{
    public class JwtTokenHandler
    {
        private readonly IConfiguration _configuration;
        public JwtTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(int userID)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userID", userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = "esa.com",
                Issuer = "esa.com", 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
