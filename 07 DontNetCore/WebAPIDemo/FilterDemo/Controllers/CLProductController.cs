using FilterDemo.BL;
using FilterDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilterDemo.Controllers
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
