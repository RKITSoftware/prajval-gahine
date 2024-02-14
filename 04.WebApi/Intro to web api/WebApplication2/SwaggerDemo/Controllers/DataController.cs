using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwaggerDemo.Controllers
{
    public class DataController : ApiController
    {
        [Route("api/data/getdata")]
        [HttpGet]
        public IHttpActionResult GetData()
        {
            return Ok("Here is ur data");
        }
    }
}
