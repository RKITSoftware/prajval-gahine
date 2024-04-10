using Microsoft.AspNetCore.Mvc;
using RoutingDemo.BL;
using RoutingDemo.Models;

namespace RoutingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            BLProduct bLProduct = new BLProduct();
            List<PRD01>? lstProduct = bLProduct.FetchProductList();
            return Ok(lstProduct);
        }
    }
}
