using DgServer.Data;
using DgServer.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DgServer.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private DataStore _dataStore;

        public UserController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet("")]
        public IActionResult GetAll(int skip = 0, int take = 5)
        {
            return Ok(_dataStore.LstUser.Skip(skip).Take(take));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dataStore.LstUser.FirstOrDefault(user => user.Id == id));
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] User user)
        {
            user.Id = _dataStore.nextUserId++;

            _dataStore.LstUser.Add(user);

            return Ok(user);
        }
    }
}
