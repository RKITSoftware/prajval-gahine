using DgServer.Data;
using DgServer.Models.Entity;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll(int skip = 0, int take = 5)
        {
            return Ok(_dataStore.LstOrder.Skip(skip).Take(take));
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

            order.Amount = _dataStore.LstProduct.Where(p => order.LstProductId.Contains(p.Id)).Sum(p => p.Price);

            if(order.DeliveryAddress == null)
            {
                order.DeliveryAddress = _dataStore.LstUser.Where(u => u.Id == order.UserId).FirstOrDefault().PermanentAddress;
            }

            _dataStore.LstOrder.Add(order);
            return Ok(order);
        }
    }
}
