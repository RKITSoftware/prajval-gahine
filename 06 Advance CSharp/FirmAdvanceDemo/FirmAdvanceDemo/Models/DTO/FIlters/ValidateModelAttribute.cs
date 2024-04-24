using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FirmAdvanceDemo.Models.DTO.FIlters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            if(!context.ModelState.IsValid)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, context.ModelState);
            }
        }
    }
}