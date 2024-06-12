using DgServer.Data;
using DgServer.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DgServer.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private DataStore _dataStore;

        public ProductController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet("")]
        public IActionResult GetAll(int skip = 0, int take = 5)
        {
            return Ok(_dataStore.LstProduct.Skip(skip).Take(take));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dataStore.LstProduct.FirstOrDefault(product => product.Id == id));
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Product product)
        {
            product.Id = _dataStore.nextUserId++;

            _dataStore.LstProduct.Add(product);

            return Ok(product);
        }
    }
}
