using CachingDemo.Caching;
using System;
using System.Web.Http;

namespace CachingDemo.Controllers
{
    /// <summary>
    /// DataController class to handle request on controller = data
    /// </summary>
    [RoutePrefix("api/data")]
    public class DataController : ApiController
    {
        /// <summary>
        /// DataController Action to get a sample data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getdata")]
        [CachingAttribute]
        public Object GetData()
        {
            return new { name = "prajval", age = 22 };
        }
    }
}
