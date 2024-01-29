using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAppInfo()
        {
            return Ok("Welcome to FirmAdvanceDemo");
        }
    }
}
