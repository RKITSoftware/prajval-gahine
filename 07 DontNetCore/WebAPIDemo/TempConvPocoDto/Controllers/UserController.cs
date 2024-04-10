using Microsoft.AspNetCore.Mvc;
using TempConvPocoDto.DTO;
using TempConvPocoDto.Models;

namespace TempConvPocoDto.Controllers
{
    public class UserController : Controller
    {
        public object Index([FromBody] dtoADM01USR01 dtoAU)
        {
            // poco
            ADM01USR01 adminUser = new ADM01USR01
            {
                admin = ModelConversion<ADM01, dtoADM01>.ToPOCO(dtoAU.admin),
                user = ModelConversion<USR01, dtoUSR01>.ToPOCO(dtoAU.user)
            };
            return adminUser;
        }
    }
}
