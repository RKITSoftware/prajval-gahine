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
            BLEmployee bLEmployee = new BLEmployee();
            List<EMP01> lstEmployee = bLEmployee.GetEmployees(); ;

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
            BLEmployee bLEmployee = new BLEmployee();
            ResponseStatusInfo responseStatusInfo = bLEmployee.AddAttendance(userId);

            if (responseStatusInfo.IsRequestSuccessful == false)
            {
                // attendance filling failed
                return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));

            }
            return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, null));
        }

        // get employee attendance
        /// <summary>
        /// Employee controller action to get an employee attendances
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("attendance/{userId}")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendance(int userId)
        {
            BLEmployee blEmployee = new BLEmployee();
            ResponseStatusInfo rsi = blEmployee.FetchEmployeeId(userId);

            if (!rsi.IsRequestSuccessful)
            {
                return ResponseMessage(Request.CreateErrorResponse(rsi.StatusCode, rsi.Message));
            }
            int employeeId = (int)rsi.Data;

            rsi = blEmployee.FetchAttendance(employeeId);

            return Ok(ResponseWrapper.Wrap(rsi.Message, rsi.Data));
        }

        // get employee attendance
        /// <summary>
        /// Employee controller action to get an employee attendances
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("attendance/{id}")]
        [BasicAuthorization(Roles = "employee")]
        public IHttpActionResult GetAttendance()
        {
            // get claims from identity attached to User or Thread.CurrentPrincipal
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;

            // get user id from identity
            int userId = int.Parse(claims.Where(c => c.Type == "r01f01")
                   .Select(c => c.Value).SingleOrDefault());


            BLEmployee blEmployee = new BLEmployee();
            ResponseStatusInfo rsi = blEmployee.FetchEmployeeId(userId);

            if (!rsi.IsRequestSuccessful)
            {
                return ResponseMessage(Request.CreateErrorResponse(rsi.StatusCode, rsi.Message));
            }
            int employeeId = (int)rsi.Data;

            rsi = blEmployee.FetchAttendance(employeeId);

            return Ok(ResponseWrapper.Wrap(rsi.Message, rsi.Data));
        }
    }
}
