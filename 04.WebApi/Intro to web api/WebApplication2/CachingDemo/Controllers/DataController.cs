using CachingDemo.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CachingDemo.Controllers
{
    [RoutePrefix("api/data")]
    public class DataController : ApiController
    {
        [HttpGet]
        [Route("getdata")]
        [CachingAttribute]
        public Object GetData()
        {
            return new { name = "prajval", age = 22 };
        }
    }
}
