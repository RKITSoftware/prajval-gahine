using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Authentication;

namespace WebApplication2.Controllers
{
    public class CLDefaultController : ApiController
    {
        [HttpGet]
        [Route("")]
        //[Authorize]
        [BasicAuthenticationAttribute]
        public IHttpActionResult GetData()
        {
            return Ok("Welcome to web application, try api/{controller}/{id} to enjoy api service");
        }
    }
}
