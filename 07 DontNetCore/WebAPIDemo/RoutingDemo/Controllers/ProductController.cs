using Microsoft.AspNetCore.Mvc;
using RoutingDemo.BL;
using RoutingDemo.Models;

namespace RoutingDemo.Controllers
{
    /// <summary>
    /// Product controller
    /// </summary>        
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Method to get list of products
        /// </summary>
        /// <returns>List of Product</returns>
        [HttpGet]
        [Route("")]
        [Route("getAll")]
        public IActionResult Get()
        {
            BLProduct bLProduct = new BLProduct();
            List<PRD01>? lstProduct = bLProduct.FetchProductList();
            return Ok(lstProduct);
        }

        /// <summary>
        /// Method to get a products by id
        /// </summary>
        /// <returns>Product instance</returns>
        [HttpGet]
        [Route("{id:int:min(1)}")]
        [Route("getProduct/{id:int:min(1)}")]
        public IActionResult GetProduct(int id)
        {
            BLProduct bLProduct = new BLProduct();
            PRD01? product = bLProduct.FetchProduct(id);
            return Ok(product);
        }
    }
}
