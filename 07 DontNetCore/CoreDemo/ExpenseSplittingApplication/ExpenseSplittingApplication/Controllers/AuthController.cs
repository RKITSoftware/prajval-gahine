using ExpenseSplittingApplication.BL.Common.Service;
using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseSplittingApplication.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related operations.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        /// <summary>
        /// The JWT token handler.
        /// </summary>
        private readonly JwtTokenHandler _tokenHandler;

        /// <summary>
        /// The user service interface.
        /// </summary>
        private readonly IUSR01Service _userHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="tokenHandler">The JWT token handler.</param>
        /// <param name="userHandler">The user service interface.</param>
        public AuthController(JwtTokenHandler tokenHandler, IUSR01Service userHandler)
        {
            _tokenHandler = tokenHandler;
            _userHandler = userHandler;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="login">The login credentials.</param>
        /// <returns>An <see cref="IActionResult"/> containing the JWT token or an unauthorized response.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Login login)
        {
            int userId = _userHandler.GetUserId(login.Username, login.Password);
            if (userId < 1)
            {
                return Unauthorized();
            }
            string token = _tokenHandler.GenerateToken(userId);
            return Ok(token);
        }
    }
}
