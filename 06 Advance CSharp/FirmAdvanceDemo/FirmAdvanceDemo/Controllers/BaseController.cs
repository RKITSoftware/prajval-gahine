using FirmAdvanceDemo.Utitlity;
using System;
using System.Net.Http;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    public class BaseController : ApiController
    {
        /// <summary>
        /// Represent status of request response pipeline
        /// </summary>
        internal ResponseStatusInfo _statusInfo;

        /// <summary>
        /// Default Controller for BaseController
        /// </summary>
        public BaseController()
        {
            _statusInfo = new ResponseStatusInfo();
        }

        /// <summary>
        /// Method used to have consistent (uniform) returns from all controller actions
        /// </summary>
        /// <param name="responseStatusInfo">ResponseStatusInfo instance containing response specific information</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [Obsolete]
        [NonAction]
        protected IHttpActionResult Returner(ResponseStatusInfo statusInfo)
        {
            if (statusInfo.IsRequestSuccessful)
            {
                return Ok(ResponseWrapper.Wrap(statusInfo.Message, statusInfo.Data));
            }
            return ResponseMessage(Request.CreateErrorResponse(statusInfo.StatusCode, statusInfo.Message));
        }

        [NonAction]
        protected IHttpActionResult Returner()
        {
            return Returner(_statusInfo);
        }
    }
}