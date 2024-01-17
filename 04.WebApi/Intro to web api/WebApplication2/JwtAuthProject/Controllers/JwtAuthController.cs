using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using JwtAuthProject.Authentication;

namespace JwtAuthProject.Controllers
{
    /// <summary>
    /// Jwt controller to handle jwt specific requests like generate token
    /// </summary>
    public class JwtAuthController : ApiController
    {
        /// <summary>
        /// Action to get token after basic authentication
        /// </summary>
        /// <returns>return a jwt</returns>
        [HttpGet]
        [Route("api/gettoken")]
        [BasicAuthenticationAttribute]
        public Object GetToken()
        {
            string SecretKey = "mykeyisthisbecausemykeyisthisbecause";
            string issuer = "JwtAuthProject";

            // get claims from identity attached to User or Thread.CurrentPrincipal
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;

            // get user id from identity
            int userId = int.Parse(claims.Where(c => c.Type == "r01f01")
                   .Select(c => c.Value).SingleOrDefault());

            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            // add claims to claimList
            var jwtClaims = new List<Claim>();
            //claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            jwtClaims.Add(new Claim("valid", "1"));
            jwtClaims.Add(new Claim("r01f01", userId.ToString()));

            // token object
            var token = new JwtSecurityToken(
                issuer, 
                issuer, 
                jwtClaims, 
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: Credentials);

            // create jwt token
            string jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            return new { data = jwt_token };
        }
    }
}
