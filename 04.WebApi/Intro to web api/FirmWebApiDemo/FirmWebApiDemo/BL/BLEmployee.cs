using FirmWebApiDemo.Models;
using FirmWebApiDemo.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace FirmWebApiDemo.BL
{
    public class BLEmployee
    {
        /// <summary>
        /// File location to Employee.json file
        /// </summary>
        private static readonly string EmployeeFilePath = HttpContext.Current.Server.MapPath(@"~/data/Employee.json");


        /// <summary>
        /// BL method to add todays attendance of an employee
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>ResponseStatusInfo object that encapsulates info to create response accordingly</returns>
        public ResponseStatusInfo AddAttendance(int userId)
        {
            // access Attendance.json file and if current employee's attendance is not already filled for todays date
            // then add the attendance to Attendance.json file
            // else don't

            BLAttendance bLAttendance = new BLAttendance();

            List<ATD01> lstAttendance = bLAttendance.GetAttendances();

            // now get employee id for given userid from User-Employee junction
            BLUser_Employee bLUser_Employee = new BLUser_Employee();
            List<USR01_EMP01> lstUserEmployee = bLUser_Employee.GetUserEmployees();

            USR01_EMP01 UserEmployee = lstUserEmployee.FirstOrDefault(ue => ue.p01f01 == userId);

            if (UserEmployee == null)    // no employee is associated with given userid
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "You are not an employee",
                    Data = null
                };
            }

            int EmployeeId = UserEmployee.p01f02;


            if (lstAttendance.Any(attendance => attendance.d01f02 == EmployeeId && attendance.d01f03.Date == DateTime.Now.Date))
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Your attendance for today was already filled",
                    Data = null
                };
            }

            // get nextAttendanceId
            int NextAttendanceId = -1;
            if (lstAttendance.Count == 0)
            {
                NextAttendanceId = 1;
            }
            else
            {
                NextAttendanceId = lstAttendance[lstAttendance.Count - 1].d01f01 + 1;
            }

            // create an attendance .net object and save it to Attendance.json file
            ATD01 newAttendance = new ATD01(NextAttendanceId, EmployeeId, DateTime.Now);
            bLAttendance.SetAttendance(newAttendance);

            return new ResponseStatusInfo()
            {
                IsRequestSuccessful = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Today's attendance was filled successfully",
                Data = null
            };
        }

        /// <summary>
        /// BL method to fetch an employee attendances (by admin)
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>ResponseStatusInfo object that encapsulates info to create response accordingly</returns>
        public ResponseStatusInfo FetchAttendances(int EmployeeId)
        {
            BLEmployee blEmployee = new BLEmployee();
            // check if employee exists
            if (!blEmployee.Exists(EmployeeId))
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"No Employee exists with employee id: {EmployeeId}",
                    Data = null
                };
                //return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No Employee exists with employee id: {EmployeeId}"));
            }

            BLAttendance bLAttendance = new BLAttendance();

            List<ATD01> lstAttendance = bLAttendance.GetAttendances();

            List<DateTime> lstEmployeeAttendance = lstAttendance.Where(attendance => attendance.d01f02 == EmployeeId)
                .Select(attendance => attendance.d01f03)
                .ToList();

            return new ResponseStatusInfo()
            {
                IsRequestSuccessful = true,
                StatusCode = HttpStatusCode.OK,
                Message = $"Attendance List of Employee with Employee id: {EmployeeId}",
                Data = new { attendances = lstEmployeeAttendance }
            };
            //return Ok(ResponseWrapper.Wrap($"Attendance List of Employee with Employee id: {EmployeeId}", new { attendances = lstEmployeeAttendance }));

        }

        /// <summary>
        /// BL method to fetch an employee attendances (employee himself)
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>ResponseStatusInfo object that encapsulates info to create response accordingly</returns>
        public ResponseStatusInfo FetchAttendance(int UserId)
        {

            BLAttendance bLAttendance = new BLAttendance();
            List<ATD01> lstAttendance = bLAttendance.GetAttendances();

            // now get employee id for given userid from User-Employee junction
            BLUser_Employee bLUser_Employee = new BLUser_Employee();
            List<USR01_EMP01> lstUserEmployee = bLUser_Employee.GetUserEmployees();

            USR01_EMP01 UserEmployee = lstUserEmployee.FirstOrDefault(ue => ue.p01f01 == UserId);

            if (UserEmployee == null)    // no employee is associated with given userid
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "You are not an employee",
                    Data = null
                };
            }

            int EmployeeId = UserEmployee.p01f02;

            List<DateTime> lstEmployeeAttendance = lstAttendance.Where(attendance => attendance.d01f02 == EmployeeId)
                .Select(attendance => attendance.d01f03)
                .ToList();

            return new ResponseStatusInfo()
            {
                IsRequestSuccessful = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Your attendance list",
                Data = new { attendances = lstEmployeeAttendance }
            };
        }


        /// <summary>
        /// Checks whether any employee exists with supplied employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Returns true if Employee exists else false</returns>
        public bool Exists(int id)
        {
            List<EMP01> lstEmployee = GetEmployees();

            return lstEmployee.Any(employee => employee.p01f01 == id);
        }


        /// <summary>
        /// Gets all employees list
        /// </summary>
        /// <returns>list of employees</returns>
        public List<EMP01> GetEmployees()
        {
            List<EMP01> employees = null;

            using (StreamReader sr = new StreamReader(EmployeeFilePath))
            {
                string employeeJson = sr.ReadToEnd();
                employees = JsonConvert.DeserializeObject<List<EMP01>>(employeeJson);
            }

            return employees;
        }

        /// <summary>
        /// Method to add employee in Employee.json file array
        /// </summary>
        /// <param name="user">An employee object of EMP01 type</param>
        public void SetEmployee(EMP01 employee)
        {
            List<EMP01> employees = GetEmployees();

            // add the user to list
            employees.Add(employee);

            using (StreamWriter sw = new StreamWriter(EmployeeFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(employees, Formatting.Indented));
            }
        }

        /// <summary>
        /// Method to add employees in Employee.json file array
        /// </summary>
        /// <param name="employee">An Employee List of EMP01 type</param>
        public void SetEmployees(List<EMP01> lstEmployee)
        {
            using (StreamWriter sw = new StreamWriter(EmployeeFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(lstEmployee, Formatting.Indented));
            }
        }
    }
}
