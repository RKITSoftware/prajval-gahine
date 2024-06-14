using DgServer.Data;
using DgServer.Models.Domain;
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
        public IActionResult GetAll(int skip = 0, int take = 10)
        {
            return Ok(new { data = _dataStore.LstProduct.Skip(skip).Take(take), totalCount = _dataStore.LstProduct.Count } );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dataStore.LstProduct.FirstOrDefault(product => product.Id == id));
        }

        [HttpGet("order/{orderId}")]
        public IActionResult GetByOrderId(int orderId)
        {
            Order order = _dataStore.LstOrder.FirstOrDefault(order => order.Id == orderId);
            return Ok(order.LstProductIdQuantity.Select(piq =>
            {
                return new ProductQuantity(_dataStore.LstProduct.FirstOrDefault(p => p.Id == piq.ProductId), piq.Quantity);
            }));
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Product product)
        {
            product.Id = _dataStore.nextProductId++;

            _dataStore.LstProduct.Add(product);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            var existingProduct = _dataStore.LstProduct.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;

            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _dataStore.LstProduct.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            _dataStore.LstProduct.Remove(product);

            return Ok(new { Message = $"Product with ID {id} deleted successfully." });
        }
    }
}
