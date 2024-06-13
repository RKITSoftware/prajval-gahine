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
        public IActionResult GetAll(int skip = 0, int take = 10, bool all = false)
        {
            if (all == true)
            {
                return Ok(_dataStore.LstUser);
            }
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

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            var existingUser = _dataStore.LstUser.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.PermanentAddress = user.PermanentAddress;

            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _dataStore.LstUser.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found." });
            }

            _dataStore.LstUser.Remove(user);

            return Ok(new { Message = $"User with ID {id} deleted successfully." });
        }
    }
}
