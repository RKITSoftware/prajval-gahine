using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseSplittingApplication.BL.Common.Service
{
    /// <summary>
    /// Service class for generating JWT tokens.
    /// </summary>
    public class JwtTokenHandler
    {
        /// <summary>
        /// Configuration interface providing access to application settings, used for JWT token generation.
        /// </summary>
        private readonly IConfiguration _configuration;


        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenHandler"/> class.
        /// </summary>
        /// <param name="configuration">The configuration containing JWT settings.</param>
        public JwtTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a JWT token for the specified user ID.
        /// </summary>
        /// <param name="userID">The ID of the user for whom the token is generated.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public string GenerateToken(int userID)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Get the JWT signing key from configuration
            byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]);

            // Create token descriptor
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

            // Create and write the JWT token
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
