using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ExpenseSplittingApplication.Models.DTO.Filters
{
    public class ValidateUserAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int userID = int.Parse(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userID")?.Value ?? "");
            DTOUSR01? user = (DTOUSR01?)context.ActionArguments["objDTOUSR01"];
            if (user != null)
            {
                if(user.R01F01 != userID)
                {
                    context.Result = new UnauthorizedObjectResult("Provided userid donot matched with authenticated userid.");
                }
            }
        }
    }
}
