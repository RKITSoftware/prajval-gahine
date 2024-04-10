using ControllerInitialization.BL;
using ControllerInitialization.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllerInitialization.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CLProductController : ControllerBase
    {
        [HttpPost]
        [Route("api/product/index")]
        public IActionResult Get(PRD01 p)
        {
            BLProduct bLProduct = new BLProduct();
            List<PRD01>? lstProduct = bLProduct.FetchProductList();
            return Ok(lstProduct);
        }
    }
}
