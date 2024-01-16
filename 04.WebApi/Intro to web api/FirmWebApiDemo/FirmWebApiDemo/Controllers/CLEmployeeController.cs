using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using FirmWebApiDemo.Authentication;
using FirmWebApiDemo.Models;
using FirmWebApiDemo.Utility;
using Newtonsoft.Json.Linq;

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
            List<EMP01> lstEmployee = EMP01.GetEmployees();

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
            // access Attendance.json file and if current employee's attendance is not already filled for todays date
            // then add the attendance to Attendance.json file
            // else don't

            List<ATD01> lstAttendance = ATD01.GetAttendances();

            // get claims from identity attached to User or Thread.CurrentPrincipal
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;

            // get user id from identity
            int userId = int.Parse(claims.Where(c => c.Type == "r01f01")
                   .Select(c => c.Value).SingleOrDefault());

            // now get employee id for given userid from User-Employee junction
            List<USR01_EMP01> lstUserEmployee = USR01_EMP01.GetUserEmployees();

            USR01_EMP01 UserEmployee = lstUserEmployee.FirstOrDefault(ue => ue.p01f01 == userId);

            if(UserEmployee == null)    // no employee is associated with given userid
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "You are not an employee"));
            }

            int EmployeeId = UserEmployee.p01f02;


            if(lstAttendance.Any(attendance => attendance.d01f02 == EmployeeId && attendance.d01f03.Date == DateTime.Now.Date))
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Your attendance for today was already filled"));
            }
            else
            {
                // get nextAttendanceId
                int NextAttendanceId = -1;
                if(lstAttendance.Count == 0)
                {
                    NextAttendanceId = 1;
                }
                else
                {
                    NextAttendanceId = lstAttendance[lstAttendance.Count - 1].d01f01 + 1;
                }

                // create an attendance .net object and save it to Attendance.json file
                ATD01 attendance = new ATD01(NextAttendanceId, EmployeeId, DateTime.Now);
                ATD01.SetAttendance(attendance);
            }
            return Ok(ResponseWrapper.Wrap("Today's attendance was filled successfully", null));
        }

        // get another employee attendance - admin
        /// <summary>
        /// Employee controller action to get an employee attendances
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("attendance/{id}")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendance(int id)
        {
            List<ATD01> lstAttendance = ATD01.GetAttendances();

            List<DateTime> lstEmployeeAttendance = lstAttendance.Where(attendance => attendance.d01f02 == id)
                .Select(attendance => attendance.d01f03)
                .ToList();

            return Ok(ResponseWrapper.Wrap($"Attendance List of Employee with Employee id: {id}", lstEmployeeAttendance));
        }

        // get my attendance
        [HttpGet]
        [Route("attendance")]
        [BasicAuthorization(Roles = "employee")]
        public IHttpActionResult GetAttendance()
        {
            List<ATD01> lstAttendance = ATD01.GetAttendances();

            // get claims from identity attached to User or Thread.CurrentPrincipal
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;

            // get user id from identity
            int userId = int.Parse(claims.Where(c => c.Type == "r01f01")
                   .Select(c => c.Value).SingleOrDefault());

            // now get employee id for given userid from User-Employee junction
            List<USR01_EMP01> lstUserEmployee = USR01_EMP01.GetUserEmployees();

            USR01_EMP01 UserEmployee = lstUserEmployee.FirstOrDefault(ue => ue.p01f01 == userId);

            if (UserEmployee == null)    // no employee is associated with given userid
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "You are not an employee"));
            }

            int EmployeeId = UserEmployee.p01f02;

            List<DateTime> lstEmployeeAttendance = lstAttendance.Where(attendance => attendance.d01f02 == EmployeeId)
                .Select(attendance => attendance.d01f03)
                .ToList();

            return Ok(ResponseWrapper.Wrap($"Your attendance list", lstEmployeeAttendance));


        }
    }
}
