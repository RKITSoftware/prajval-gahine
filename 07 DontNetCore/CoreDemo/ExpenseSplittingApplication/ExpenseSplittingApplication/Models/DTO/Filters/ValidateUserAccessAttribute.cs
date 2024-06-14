using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ExpenseSplittingApplication.Models.DTO.Filters
{
    /// <summary>
    /// Action filter attribute to validate user access based on authenticated user ID.
    /// </summary>
    public class ValidateUserAccessAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Validates user access before executing an action.
        /// </summary>
        /// <param name="context">The context for the action filter.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Get authenticated user ID from token
            int userIDFromToken = int.Parse(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userID")?.Value ?? "");

            // Initialize user ID from request body
            int userIDFromBody = 0;

            // Determine the controller name to handle different DTO types
            string controllerName = context.Controller.GetType().Name;

            // Extract user ID from DTO based on controller type
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
                    // Add more cases for other controllers if needed
            }

            // Check if user ID from request body matches authenticated user ID
            if (userIDFromBody != userIDFromToken)
            {
                context.Result = new UnauthorizedObjectResult("Provided userid does not match with authenticated userid.");
            }
        }
    }
}
