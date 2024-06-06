using ModelAndBLClassLib.BL;
using ModelAndBLClassLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllerInitialization.Controllers
{
    /// <summary>
    /// Product controller
    /// </summary>
    [ApiController]
    public class CLProductController : ControllerBase
    {
        /// <summary>
        /// BLProduct instance
        /// </summary>
        private readonly BLProduct _bLProduct;

        /// <summary>
        /// Constructor to initialize BLProduct class
        /// </summary>
        /// <param name="bLProduct"></param>
        public CLProductController(BLProduct bLProduct)
        {
            _bLProduct = bLProduct;
        }

        /// <summary>
        /// Action method to get all product
        /// </summary>
        /// <returns></returns>
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
