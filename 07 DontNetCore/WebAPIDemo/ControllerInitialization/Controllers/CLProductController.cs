using ControllerInitialization.BL;
using ControllerInitialization.Models;
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
        /// Get list of product
        /// </summary>
        /// <param name="p">Product</param>
        /// <returns>List of products</returns>
        [HttpGet]
        [Route("api/product")]
        public IActionResult Get()
        {
            BLProduct bLProduct = new BLProduct();
            List<PRD01>? lstProduct = bLProduct.FetchProductList();
            return Ok(lstProduct);
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/product")]
        public IActionResult Post([FromForm]PRD01 p)
        {
            BLProduct bLProduct = new BLProduct();
            Response response = bLProduct.Validate(p);
            if (!response.IsError)
            {
                bLProduct.PreSave(p);
                response = bLProduct.Save(p);
            }
            return Ok(response);
        }
    }
}
