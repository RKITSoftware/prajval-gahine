using DgServer.Data;
using DgServer.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DgServer.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private DataStore _dataStore;

        public OrderController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet("")]
        public IActionResult GetAll(int? skip, int? take)
        {
            if(skip == null)
            {
                return Ok(_dataStore.LstOrder);
            }
            return Ok(_dataStore.LstOrder.Skip((int)skip).Take((int)take));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dataStore.LstOrder.FirstOrDefault(user => user.Id == id));
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Order order)
        {
            order.Id = _dataStore.nextOrderId++;

            order.Amount = _dataStore.LstProduct.Where(p => order.LstProductIdQuantity.Select(p => p.ProductId).Contains(p.Id)).Sum(p => p.Price);

            if(order.DeliveryAddress == null)
            {
                order.DeliveryAddress = _dataStore.LstUser.Where(u => u.Id == order.UserId).FirstOrDefault().PermanentAddress;
            }

            _dataStore.LstOrder.Add(order);
            return Ok(order);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order order)
        {
            var existingOrder = _dataStore.LstOrder.FirstOrDefault(p => p.Id == id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.UserId = order.UserId;
            existingOrder.LstProductIdQuantity = order.LstProductIdQuantity;
            existingOrder.Amount = order.Amount = _dataStore.LstProduct.Where(p => existingOrder.LstProductIdQuantity.Select(p => p.ProductId).Contains(p.Id)).Sum(p => p.Price);
            existingOrder.DeliveryAddress = order.DeliveryAddress;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.IsDelivered = order.IsDelivered;

            return Ok(existingOrder);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _dataStore.LstOrder.FirstOrDefault(p => p.Id == id);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with ID {id} not found." });
            }

            _dataStore.LstOrder.Remove(order);

            return Ok(new { Message = $"Order with ID {id} deleted successfully." });
        }
    }
}
