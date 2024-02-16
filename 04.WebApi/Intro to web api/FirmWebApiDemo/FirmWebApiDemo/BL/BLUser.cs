using FirmWebApiDemo.Models;
using FirmWebApiDemo.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace FirmWebApiDemo.BL
{
    public class BLUser
    {

        /// <summary>
        /// File location to User.json file
        /// </summary>
        private static readonly string UserFilePath = HttpContext.Current.Server.MapPath(@"~/data/User.json");


        /// <summary>
        /// BL method to add a user
        /// </summary>
        /// <param name="newUserData">JObject that contains new user data that is to be created</param>
        /// <returns>ResponseStatusInfo object that encapsulates info to create response accordingly</returns>
        public ResponseStatusInfo AddUser(JObject newUserData)
        {
            // check if user already exists
            List<USR01> users = GetUsers();
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
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Provide employee data",
                        Data = null
                    };
                    //return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Provide employee data"));
                }

                // update USR01.json file
                SetUser(newUser);

                // check if role is employee
                if (IsEmployee && newUserData["employee"] != null)
                {
                    int nextEmployeeId = -1;

                    BLEmployee blEmployee = new BLEmployee();
                    List<EMP01> employees = blEmployee.GetEmployees();

                    // set next employee id
                    if (employees.Count == 0)
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
                    blEmployee.SetEmployee(newEmployee);

                    // update UserEmployee.json junction file
                    BLUser_Employee bLUser_Employee = new BLUser_Employee();
                    bLUser_Employee.SetUserEmployee(newUser.r01f01, newEmployee.p01f01);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        StatusCode = HttpStatusCode.OK,
                        Message = $"User and Employee has been successfully created. Your user-id is: {nextUserId} and employee-id is {nextEmployeeId}",
                        Data = newUser
                    };
                    //return Ok(ResponseWrapper.Wrap($"User and Employee has been successfully created. Your user-id is: {nextUserId} and employee-id is {nextEmployeeId}", newUser));
                }
            }
            else
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = false,
                    StatusCode = (HttpStatusCode)403,
                    Message = "User Already exists",
                    Data = null
                };
                //return ResponseMessage(Request.CreateErrorResponse((HttpStatusCode)403, "User Already exists"));
            }
            return new ResponseStatusInfo()
            {
                IsRequestSuccessful = true,
                StatusCode = HttpStatusCode.OK,
                Message = $"User (you are not an employee) has been successfully created. Your user id is: {newUser.r01f01}",
                Data = newUser
            };
        }


        /// <summary>
        /// BL method to delete a user
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>ResponseStatusInfo object that encapsulates info to create response accordingly</returns>
        public ResponseStatusInfo RemoveUser(int id)
        {

            // get user list
            List<USR01> lstUser = GetUsers();
            // check if the such user exists with r01f01 = id

            if (lstUser.Count == 1)
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Only you exists as user, cannot perform the delete process",
                    Data = null
                };
                //return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Only you exists as user, cannot perform the delete process"));
            }

            // get index
            int userIndex = lstUser.FindIndex(u => u.r01f01 == id);

            if (userIndex != -1)
            {
                // get userId to get employeeid from UserEmployee.json file
                int userId = lstUser[userIndex].r01f01;

                // get employee id
                BLUser_Employee bLUser_Employee = new BLUser_Employee();
                List<USR01_EMP01> lstUserEmployee = bLUser_Employee.GetUserEmployees();
                int EmployeeId = lstUserEmployee.FirstOrDefault(ue => ue.p01f01 == userId).p01f02;

                BLEmployee blEmployee = new BLEmployee();
                List<EMP01> lstEmployee = blEmployee.GetEmployees();
                int EmployeeIndex = lstEmployee.FindIndex(employee => employee.p01f01 == EmployeeId);
                if (EmployeeIndex != -1)
                {
                    // remove employee from lstEmployee
                    lstEmployee.RemoveAt(EmployeeIndex);

                    // update the same in Employee.json file
                    blEmployee.SetEmployees(lstEmployee);

                    BLAttendance bLAttendance = new BLAttendance();

                    // remove all employee trace from attendance too
                    List<ATD01> lstAttendance = bLAttendance.GetAttendances();

                    lstAttendance.RemoveAll(attendance => attendance.d01f02 == EmployeeId);

                    // update the same in Attendance.json file
                    bLAttendance.SetAttendances(lstAttendance);
                }

                // remove user from lstEmployee
                lstUser.RemoveAt(userIndex);

                // update the new users list in USER.json file
                SetUsers(lstUser);

                // response user deleted successfully
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "user deleted successfully",
                    Data = null
                };
                //return Ok(ResponseWrapper.Wrap("user deleted successfully", null));
            }
            return new ResponseStatusInfo()
            {
                IsRequestSuccessful = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = "User Not Found",
                Data = null
            };
            //return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found"));
        }



        /// <summary>
        /// Method to fetch all users list from USR01.json file
        /// </summary>
        /// <returns>List of user of USR01 class</returns>
        public List<USR01> GetUsers()
        {
            List<USR01> users = null;
            // get employee array from data.User.json file
            using (StreamReader sr = new StreamReader(UserFilePath))
            {
                string usersJson = sr.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<USR01>>(usersJson);
            }
            return users;
        }

        /// <summary>
        /// Method to add user in User.json file array
        /// </summary>
        /// <param name="user">An user object of USR01 type</param>
        public void SetUser(USR01 user)
        {
            List<USR01> users = GetUsers();

            // add the user to list
            users.Add(user);

            using (StreamWriter sw = new StreamWriter(UserFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(users, Formatting.Indented));
            }
        }

        /// <summary>
        /// Method to add users in User.json file array
        /// </summary>
        /// <param name="user">An user List of USR01 type</param>
        public void SetUsers(List<USR01> lstUser)
        {
            using (StreamWriter sw = new StreamWriter(UserFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(lstUser, Formatting.Indented));
            }
        }
    }
}