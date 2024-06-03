using Microsoft.AspNetCore.Mvc;

namespace ExpenseSplittingApplication.Controllers
{

    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { name = "ESA" });
        }
    }
}
