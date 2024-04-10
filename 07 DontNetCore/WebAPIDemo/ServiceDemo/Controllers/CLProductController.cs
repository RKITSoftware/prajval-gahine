using ModelAndBLClassLib.BL;
using ModelAndBLClassLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllerInitialization.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CLProductController : ControllerBase
    {
        private readonly BLProduct _bLProduct;

        public CLProductController(BLProduct bLProduct)
        {
            _bLProduct = bLProduct;
        }

        [HttpPost]
        [Route("api/product")]
        public IActionResult Get()
        {
            //BLProduct bLProduct = new BLProduct();
            List<PRD01>? lstProduct = _bLProduct.FetchProductList();
            return Ok(lstProduct);
        }
    }
}
