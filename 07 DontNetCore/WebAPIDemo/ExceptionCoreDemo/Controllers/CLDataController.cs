using Microsoft.AspNetCore.Mvc;

namespace ExceptionCoreDemo.Controllers
{
    public class CLDataController
    {
        [HttpGet]
        [Route("api/data")]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }
    }
}
