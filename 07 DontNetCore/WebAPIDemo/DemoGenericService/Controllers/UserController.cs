using DemoGenericService.BL;
using DemoGenericService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoGenericService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(BLResource<USR01> blUser)
        {
            Console.WriteLine(blUser.GetHashCode());
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("Got User");
        }
    }
}
