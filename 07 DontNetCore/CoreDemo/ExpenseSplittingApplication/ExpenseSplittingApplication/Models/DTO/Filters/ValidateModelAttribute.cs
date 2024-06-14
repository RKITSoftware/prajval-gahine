using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseSplittingApplication.Models.DTO.Filters
{
    /// <summary>
    /// Action filter attribute to validate the model state before executing an action.
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called before an action method executes. Checks if the model state is valid.
        /// If the model state is invalid, it sets the result to a BadRequestObjectResult with the model state errors.
        /// </summary>
        /// <param name="context">The context for the action filter.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
