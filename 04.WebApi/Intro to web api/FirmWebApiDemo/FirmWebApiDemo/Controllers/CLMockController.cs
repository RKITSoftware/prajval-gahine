using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirmWebApiDemo.Controllers
{
    public class CLMockController : ApiController
    {
        [HttpGet]
        public object GetData()
        {
            return new { data = "This is firm web api project" };
        }
    }
}
