using ExpenseSplittingApplication.BL.Common.Service;
using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseSplittingApplication.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly JwtTokenHandler _tokenHandler;
        private readonly IUSR01Service _userHandler;
        public AuthController(JwtTokenHandler tokenHandler, IUSR01Service userHandler)
        {
            _tokenHandler = tokenHandler;
            _userHandler = userHandler;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Login login)
        {
            USR01 objUSR01 = _userHandler.GetUser(login.Username, login.Password);
            if (objUSR01 == null)
            {
                return Unauthorized();
            }
            string token = _tokenHandler.GenerateToken(objUSR01.R01F01);
            return Ok(token);
        }
    }
}
