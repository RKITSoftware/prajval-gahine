using FirmWebApiDemo.Authentication;
using FirmWebApiDemo.BL;
using FirmWebApiDemo.Models;
using FirmWebApiDemo.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace FirmWebApiDemo.Controllers
{
    [BasicAuthentication]
    [RoutePrefix("api/clemployee")]
    public class CLEmployeeController : ApiController
    {

        //get all employees admin
        /// <summary>
        /// Employee controller action to get all employees
        /// </summary>
        /// <returns>Action result containing List of employees</returns>
        [HttpGet]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetEmployees()
        {
            List<EMP01> lstEmployee = BLEmployee.GetEmployees();

            return Ok(ResponseWrapper.Wrap("List of employees", lstEmployee));
        }

        /// <summary>
        /// Employee controller action to post todays attendance of an employee
        /// </summary>
        /// <returns>Action result containing boolean value whether attendance was successfully filled or not</returns>
        [HttpPost]
        [Route("attendance")]
        [BasicAuthorization(Roles = "employee")]
        public IHttpActionResult PostAttendance()
        {
            // get claims from identity attached to User or Thread.CurrentPrincipal
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;

            // get user id from identity
            int userId = int.Parse(claims.Where(c => c.Type == "r01f01")
                   .Select(c => c.Value).SingleOrDefault());

            // invoke AddAtendance method of BLAttendance class and get the response status info
            ResponseStatusInfo responseStatusInfo = BLEmployee.AddAttendance(userId);

            if (responseStatusInfo.IsRequestSuccessful == false)
            {
                // attendance filling failed
                return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));

            }
            return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, null));
        }

        // get another employee attendance - admin
        /// <summary>
        /// Employee controller action to get an employee attendances
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns></returns>
        [Route("attendance/{id}")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendance(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLEmployee.FetchAttendances(id);

            if (responseStatusInfo.IsRequestSuccessful == false)
            {
                return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));
            }
            return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, responseStatusInfo.Data));
        }

        // get my attendance
        [HttpGet]
        [Route("attendance")]
        [BasicAuthorization(Roles = "employee")]
        public IHttpActionResult GetAttendance()
        {

            // get claims from identity attached to User or Thread.CurrentPrincipal
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;

            // get user id from identity
            int userId = int.Parse(claims.Where(c => c.Type == "r01f01")
                   .Select(c => c.Value).SingleOrDefault());

            ResponseStatusInfo responseStatusInfo = BLEmployee.FetchAttendance(userId);

            if (responseStatusInfo.IsRequestSuccessful == false)
            {
                return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));

            }
            return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, responseStatusInfo.Data));
        }
    }
}
