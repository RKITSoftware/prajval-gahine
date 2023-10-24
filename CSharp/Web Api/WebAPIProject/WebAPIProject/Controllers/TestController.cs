using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWebAPIProject.Controllers
{
    [ApiController]
    [Route("test/[action]")]
    public class TestController : ControllerBase
    {
        public string Get()
        {
            return "Hello World from get";
        }

        // adding one more resource in the TestController class
        public string GetPrajval1()
        {
            return "Hello from get1";
        }
    }
}
