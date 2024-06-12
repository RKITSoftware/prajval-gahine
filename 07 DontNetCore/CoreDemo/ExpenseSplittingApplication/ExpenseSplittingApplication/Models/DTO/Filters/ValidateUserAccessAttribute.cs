using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ExpenseSplittingApplication.Models.DTO.Filters
{
    public class ValidateUserAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int userIDFromToken = int.Parse(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userID")?.Value ?? "");
            int userIDFromBody = 0;

            string controllerName = context.Controller.GetType().Name;

            switch (controllerName)
            {
                case "CLUSR01Controller":
                    DTOUSR01? user = (DTOUSR01?)context.ActionArguments["objDTOUSR01"];
                    userIDFromBody = user.R01F01;
                    break;
                case "CLEXP01Controller":
                    DTOEXC? dtoEXC = (DTOEXC?)context.ActionArguments["dtoEXC"];
                    userIDFromBody = dtoEXC.ObjDTOEXP01.P01F02;
                    break;

            }
            if (userIDFromBody != userIDFromToken)
            {
                context.Result = new UnauthorizedObjectResult("Provided userid donot matched with authenticated userid.");
            }
        }
    }
}
