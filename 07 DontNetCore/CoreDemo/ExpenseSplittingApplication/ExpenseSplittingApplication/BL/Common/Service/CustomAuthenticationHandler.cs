﻿using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models.POCO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ExpenseSplittingApplication.BL.Common.Service
{
    /// <summary>
    /// Custom authentication handler for handling Basic Authentication scheme.
    /// </summary>
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        /// <summary>
        /// The service interface for user-related operations, used for validating user credentials during authentication.
        /// </summary>
        private readonly IUSR01Service _userService;


        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthenticationHandler"/> class.
        /// </summary>
        /// <param name="options">The monitor for the authentication scheme options.</param>
        /// <param name="logger">The logger factory.</param>
        /// <param name="encoder">The URL encoder.</param>
        /// <param name="clock">The system clock.</param>
        /// <param name="userService">The user service for validating credentials.</param>
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

        /// <summary>
        /// Handles authentication asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous authentication operation.</returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authentication Header not found."));
            }

            string authHeader = Request.Headers["Authorization"].ToString();
            AuthenticationHeaderValue authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);

            if (authHeaderValue.Scheme != "Basic")
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid authentication schema."));
            }

            string[] credentials = Encoding.UTF8
                .GetString(Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty))
                .Split(':');

            string username = credentials[0];
            string password = credentials[1];

            // Validate user credentials using injected user service
            USR01 objUSR01 = _userService.GetUser(username, password);

            if (objUSR01 == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password."));
            }

            // Create claims for authenticated user
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
