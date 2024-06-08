
using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models.POCO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ExpenseSplittingApplication.BL.Common.Service
{
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUSR01Service _userService;
        public CustomAuthenticationHandler(
                IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger,
                UrlEncoder encoder,
                ISystemClock clock,
                IUSR01Service userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authentication Header not found."));
            }

            string authHeader = Request.Headers["Authorization"].ToString();
            AuthenticationHeaderValue authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);

            if(authHeaderValue.Scheme != "Basic")
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid authentication schema."));
            }

            string[] credentials = Encoding.UTF8
            .GetString(Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty))
            .Split(':');

            string username = credentials[0];
            string password = credentials[1];

            USR01 objUSR01 = _userService.GetUser(username, password);

            if (objUSR01 == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password."));
            }

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, objUSR01.R01F02)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
