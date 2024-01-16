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

            USR01 user = null;

            // if not then get next user id
            if (existingUser == null)
            {
                // create a new user object and appen it to User.json array
                int nextUserId = users[users.Count - 1].r01f01 + 1;

                // create JObject of similar structure like USR01

                ((JObject)newUserData["user"]).Add(new JProperty("r01f01", nextUserId.ToString()));

                // create .net USR01 object
                user = ((JObject)newUserData["user"]).ToObject<USR01>();

                // update USR01.json file
                USR01.SetUser(user);

                // check if role is employee
                if (newUserData["employee"] != null)
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
                    EMP01.SetEmployee(employeeJson.ToObject<EMP01>());

                    return Ok(ResponseWrapper.Wrap($"User and Employee has been successfully created. Your user-id is: {nextUserId} and employee-id is {nextEmployeeId}", user));
                }
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse((HttpStatusCode)403, "User Already exists"));
            }
            return Ok(ResponseWrapper.Wrap($"User has been successfully created. Your user id is: {user.r01f01}", user));
        }


        [HttpDelete]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult DeleteUser(int id)
        {
            // get user lis
            List<USR01> users = USR01.GetUsers();
            // check if the such user exists with r01f01 = id

            if(users.Count == 1)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Only you exists as user, cannot perform the delete process"));
            }

            // get index
            int userIndex = users.FindIndex(u => u.r01f01 == id);

            if(userIndex != -1)
            {
                users.RemoveAt(userIndex);

                // update the new users list in USER.json file
                USR01.SetUsers(users);

                // response user deleted successfully
                return Ok(ResponseWrapper.Wrap("user deleted successfully", null));
            }
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found"));
        }
    }
}
