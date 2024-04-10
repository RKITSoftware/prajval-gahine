using PocoDtoConversion.BL;
using PocoDtoConversion.Models;
using Microsoft.AspNetCore.Mvc;
using PocoDtoConversion.conversion;
using PocoDtoConversion.Models.dto;

namespace PocoDtoConversion.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CLProductController : ControllerBase
    {
        [HttpGet]
        [Route("api/product/index")]
        public IActionResult Get()
        {
            BLProduct bLProduct = new BLProduct();
            List<PRD01>? lstProduct = bLProduct.FetchProductList();
            return Ok(lstProduct);
        }


        [HttpPost]
        [Route("api/convert/pocoToDto/product")]
        public object pocoToDtoProduct(PRD01 product)
        {
            return ModelConversion<PRD01, PRD01DTO>.ToDTO(product);
        }

        [HttpPost]
        [Route("api/convert/pocoToDto/user")]
        public object pocoToDtoUser(USR01 user)

        {
            return ModelConversion<USR01, USR01DTO>.ToDTO(user);
        }

        [HttpPost]
        [Route("api/convert/dtoToPoco/product")]
        public object dtoToPocoProduct(PRD01DTO productDto)
        {
            return ModelConversion<PRD01, PRD01DTO>.ToPOCO(productDto);
        }

        [HttpPost]
        [Route("api/convert/dtoToPoco/user")]
        public object dtoToPocoUser(USR01DTO userDto)

        {
            return ModelConversion<USR01, USR01DTO>.ToPOCO(userDto);
        }
    }
}
