using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController1 : ControllerBase
    {
        [Route("get")]
        public string get()
        {
            return "Hello";
        }
    }
}
