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
