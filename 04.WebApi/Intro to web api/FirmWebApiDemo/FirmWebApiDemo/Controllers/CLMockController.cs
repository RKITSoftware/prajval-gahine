using System.Web.Http;

namespace FirmWebApiDemo.Controllers
{
    /// <summary>
    /// Mock Controller class to check is project live
    /// </summary>
    public class CLMockController : ApiController
    {
        /// <summary>
        /// Method to get a mock data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/mock/getdata")]
        public object GetData()
        {
            return new { data = "This is firm web api project" };
        }
    }
}
