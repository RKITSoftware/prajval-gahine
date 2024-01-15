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
using CustomJwtAuth.Authentication;

namespace CustomJwtAuth.Controllers
{
    public class TokenController : ApiController
    {
        [HttpGet]
        [Route("api/gettoken")]
        [BasicAuthenticationAttribute]
        public Object GetToken()
        {
            string SecretKey = "mykeyisthisbecausemykeyisthisbecause";
            string issuer = "prajvalgahine";

            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            // add claims to claimList
            var claims = new List<Claim>();
            //claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim("valid", "1"));
            claims.Add(new Claim("userid", "1"));
            claims.Add(new Claim("name", "prajval"));

            // token object
            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credentials);

            // create jwt token
            string jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            return new { data = jwt_token };
        }
    }
}
