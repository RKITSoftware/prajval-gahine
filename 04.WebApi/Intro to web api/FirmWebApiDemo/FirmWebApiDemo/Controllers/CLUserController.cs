using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirmWebApiDemo.Models;
using FirmWebApiDemo.Authentication;
using FirmWebApiDemo.Utility;
using Newtonsoft.Json.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FirmWebApiDemo.Controllers
{
    [BasicAuthentication]
    public class CLUserController : ApiController
    {
        /// <summary>
        /// Http Get action to get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetUsers()
        {
            List<USR01> users = USR01.GetUsers();
            return Ok(ResponseWrapper.Wrap("List of users", users));
        }

        /// <summary>
        /// Http Post action method to create a user
        /// </summary>
        /// <param name="newUserData"></param>
        /// <returns>Object that can be referred by IHttpActionResult</returns>
        [HttpPost]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult PostUser(JObject newUserData)
        {
            // check if user already exists
            List<USR01> users = USR01.GetUsers();
            USR01 existingUser = users.FirstOrDefault(u => u.r01f02 == ((JObject)newUserData["user"])["r01f02"].ToString());

            USR01 newUser = null;

            // if not then get next user id
            if (existingUser == null)
            {
                // create a new user object and appen it to User.json array
                int nextUserId = users[users.Count - 1].r01f01 + 1;

                // create JObject of similar structure like USR01

                ((JObject)newUserData["user"]).Add(new JProperty("r01f01", nextUserId.ToString()));

                // create .net USR01 object
                newUser = ((JObject)newUserData["user"]).ToObject<USR01>();

                bool IsEmployee = newUser.r01f04.Split(',').Any(role => role == "employee");

                if (IsEmployee && newUserData["employee"] == null)
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Provide employee data"));
                }

                // update USR01.json file
                USR01.SetUser(newUser);

                // check if role is employee
                if (IsEmployee && newUserData["employee"] != null)
                {
                    int nextEmployeeId = -1;
                    List<EMP01> employees = EMP01.GetEmployees();

                    // set next employee id
                    if(employees.Count == 0)
                    {
                        nextEmployeeId = 101;
                    }
                    else
                    {
                        nextEmployeeId = employees[employees.Count - 1].p01f01 + 1;
                    }

                    JObject employeeJson = ((JObject)newUserData["employee"]);

                    // add employee id to Employee json
                    employeeJson.Add(new JProperty("p01f01", nextEmployeeId.ToString()));

                    // save this employee to Employee.json
                    EMP01 newEmployee = employeeJson.ToObject<EMP01>();
                    EMP01.SetEmployee(newEmployee);

                    // update UserEmployee.json junction file
                    USR01_EMP01.SetUserEmployee(newUser.r01f01, newEmployee.p01f01);

                    return Ok(ResponseWrapper.Wrap($"User and Employee has been successfully created. Your user-id is: {nextUserId} and employee-id is {nextEmployeeId}", newUser));
                }
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse((HttpStatusCode)403, "User Already exists"));
            }
            return Ok(ResponseWrapper.Wrap($"User (you are not an employee) has been successfully created. Your user id is: {newUser.r01f01}", newUser));
        }


        [HttpDelete]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult DeleteUser(int id)
        {
            // get user list
            List<USR01> lstUser = USR01.GetUsers();
            // check if the such user exists with r01f01 = id

            if(lstUser.Count == 1)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Only you exists as user, cannot perform the delete process"));
            }

            // get index
            int userIndex = lstUser.FindIndex(u => u.r01f01 == id);

            if(userIndex != -1)
            {
                // get userId to get employeeid from UserEmployee.json file
                int userId = lstUser[userIndex].r01f01;

                // get employee id
                List<USR01_EMP01> lstUserEmployee = USR01_EMP01.GetUserEmployees();
                int EmployeeId = lstUserEmployee.FirstOrDefault(ue => ue.p01f01 == userId).p01f02;

                List<EMP01> lstEmployee = EMP01.GetEmployees();
                int EmployeeIndex = lstEmployee.FindIndex(employee => employee.p01f01 == EmployeeId);
                if(EmployeeIndex != -1)
                {
                    // remove employee from lstEmployee
                    lstEmployee.RemoveAt(EmployeeIndex);

                    // update the same in Employee.json file
                    EMP01.SetEmployees(lstEmployee);

                    // remove all employee trace from attendance too
                    List<ATD01> lstAttendance = ATD01.GetAttendances();

                    lstAttendance.RemoveAll(attendance => attendance.d01f02 == EmployeeId);

                    // update the same in Attendance.json file
                    ATD01.SetAttendances(lstAttendance);
                }

                // remove user from lstEmployee
                lstUser.RemoveAt(userIndex);

                // update the new users list in USER.json file
                USR01.SetUsers(lstUser);

                // response user deleted successfully
                return Ok(ResponseWrapper.Wrap("user deleted successfully", null));
            }
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found"));
        }
    }
}
