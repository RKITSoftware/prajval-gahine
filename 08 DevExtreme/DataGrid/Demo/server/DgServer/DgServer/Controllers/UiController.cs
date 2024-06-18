using DgServer.Data;
using Microsoft.AspNetCore.Mvc;

namespace DgServer.Controllers
{
    [ApiController]
    [Route("api/ui")]
    public class UiController : Controller
    {
        private readonly DataStore _dataStore;

        public UiController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet("menu")]
        public IActionResult GetMenus()
        {
            return Ok(_dataStore.LstMenu);
        }
    }
}
