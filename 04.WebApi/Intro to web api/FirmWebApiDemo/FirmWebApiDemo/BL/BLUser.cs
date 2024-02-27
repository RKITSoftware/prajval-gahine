using FirmWebApiDemo.Exceptions.CustomException;
using FirmWebApiDemo.Models;
using FirmWebApiDemo.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;

namespace FirmWebApiDemo.BL
{
    public class BLUser
    {
        /// <summary>
        /// File location to User.json file
        /// </summary>
        public static readonly string UserFilePath = HttpContext.Current.Server.MapPath(@"~/App_Data/User.json");

        /// <summary>
        /// Get User based on userId
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>USR01 instance if user is found else null</returns>
        public USR01 GetUser(int userId)
        {
            List<USR01> lstUser = GetUsers();

            USR01 user = lstUser.Where(u => u.r01f01 == userId).SingleOrDefault();

            return user;
        }

        /// <summary>
        /// Mehtod to get username from user ID
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>ResponseStatusInfo object that encapsulates info to create response accordingly</returns>
        public ResponseStatusInfo GetUsername(int userId)
        {
            List<USR01> lstUser = GetUsers();
            string username = lstUser.Where(user => user.r01f01 == userId)
                .Select(user => user.r01f02).SingleOrDefault();
            if (username != null && username != string.Empty)
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = $"Username for User ID: {userId}",
                    Data = username
                };
            }
            throw new UsernameNotFoundException("Username not found");
            //return new ResponseStatusInfo()
            //{
            //    IsRequestSuccessful = false,
            //    StatusCode = HttpStatusCode.NotFound,
            //    Message = "Username not found",
            //    Data = null
            //};
        }

        /// <summary>
        /// BL method to add a user
        /// </summary>
        /// <param name="UserEmployee">USREMP instance that contains info to create new user and/or new employee</param>
        /// <returns>ResponseStatusInfo object that encapsulates info to create response accordingly</returns>
        public ResponseStatusInfo AddUser(USREMP UserEmployee)
        {
            // check if user already exists
            List<USR01> lstUser = GetUsers();
            USR01 user = lstUser.FirstOrDefault(u => u.r01f02 == UserEmployee.user.r01f02);

            // if not then get next user id
            if (user == null)
            {
                user = UserEmployee.user;

                // create a new user object and append it to User.json array
                int nextUserId = lstUser[lstUser.Count - 1].r01f01 + 1;

                // set nextUser
                user.r01f01 = nextUserId;

                bool IsEmployee = user.r01f04.Split(',').Any(role => role == "employee");
                EMP01 employee = UserEmployee.employee;

                if (IsEmployee && employee == null)
                {
                    // user want's to be employee, but haven't provided employee data
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
                SetUser(user);

                // check if role is employee
                if (IsEmployee && employee != null)
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

                    // add employee id to Employee object
                    employee.p01f01 = nextEmployeeId;

                    // save this employee to Employee.json
                    blEmployee.SetEmployee(employee);

                    // update UserEmployee.json junction file
                    BLUserEmployee bLUser_Employee = new BLUserEmployee();
                    bLUser_Employee.SetUserEmployee(new UIDEID01() { d01f01 = user.r01f01, d01f02 = employee.p01f01 });
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        StatusCode = HttpStatusCode.OK,
                        Message = $"User and Employee has been successfully created. Your user-id is: {nextUserId} and employee-id is {nextEmployeeId}",
                        Data = user
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
                Message = $"User (not an employee) has been successfully created. Your user id is: {user.r01f01}",
                Data = user
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
                BLUserEmployee bLUser_Employee = new BLUserEmployee();
                List<UIDEID01> lstUserEmployee = bLUser_Employee.GetUserEmployees();
                int EmployeeId = lstUserEmployee.FirstOrDefault(ue => ue.d01f01 == userId).d01f02;

                BLEmployee blEmployee = new BLEmployee();
                List<EMP01> lstEmployee = blEmployee.GetEmployees();
                int EmployeeIndex = lstEmployee.FindIndex(employee => employee.p01f01 == EmployeeId);
                if (EmployeeIndex != -1)
                {
                    // remove employee from lstEmployee
                    lstEmployee.RemoveAt(EmployeeIndex);

                    // remove all employee trace from attendance too
                    BLAttendance bLAttendance = new BLAttendance();
                    List<ATD01> lstAttendance = bLAttendance.GetAttendances();
                    lstAttendance.RemoveAll(attendance => attendance.d01f02 == EmployeeId);

                    // update the same in Attendance.json file
                    bLAttendance.SetAttendances(lstAttendance);

                    // remove the user, employee trace from junction file (UIDEID.json)
                    BLUserEmployee blUserEmployee = new BLUserEmployee();
                    List<UIDEID01> lstUidEid = blUserEmployee.GetUserEmployees();
                    lstUidEid.RemoveAt(lstUidEid.FindIndex(UidEid => UidEid.d01f01 == id));

                    // update the same in UserEmployee.json file
                    blUserEmployee.SetUserEmployees(lstUidEid);

                    // update the same in Employee.json file
                    blEmployee.SetEmployees(lstEmployee);
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
        /// Method to fetch all users list from USR01.
        /// file
        /// </summary>
        /// <returns>List of user of USR01 class</returns>
        public List<USR01> GetUsers()
        {
            List<USR01> lstUser = (List<USR01>)CacheManager.AppCache.Get("lstUser");
            if (lstUser == null)
            {
                // get employee array from data.User.json file
                using (StreamReader sr = new StreamReader(UserFilePath))
                {
                    string usersJson = sr.ReadToEnd();
                    lstUser = JsonConvert.DeserializeObject<List<USR01>>(usersJson);
                }

                CacheDependency cacheDependency = new CacheDependency(
                    UserFilePath,
                    DateTime.Now.AddSeconds(20)
                );

                CacheManager.AppCache.Insert(
                    "lstUser",
                    lstUser,
                    cacheDependency,
                    DateTime.MaxValue,
                    new TimeSpan(0, 0, 20)
                );
            }
            return lstUser;
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