using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FirmAdvanceDemo.Models.DTO.FIlters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        #region Public Methods
        /// <summary>
        /// Attribute to validate model
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, context.ModelState);
            }
        }
        #endregion
    }
}