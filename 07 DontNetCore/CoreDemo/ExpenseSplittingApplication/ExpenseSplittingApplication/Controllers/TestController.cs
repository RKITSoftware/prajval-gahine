using Microsoft.AspNetCore.Mvc;

namespace ExpenseSplittingApplication.Controllers
{
    /// <summary>
    /// Controller for testing purposes.
    /// </summary>
    public class TestController : Controller
    {
        /// <summary>
        /// Action method that returns a JSON response containing a name.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the JSON response.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { name = "ESA" });
        }
    }
}
