using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VersioningDemo.Models;

namespace VersioningDemo.Controllers
{


    /// <summary>
    /// Employee controller of version v2
    /// </summary>
    public class EmployeeV2Controller : ApiController
    {
        /// <summary>
        /// Get employee list
        /// </summary>
        /// <returns>list of employee</returns>
        [HttpGet]
        //[Route("getemployees")]
        public List<EmployeeV2> GetEmployees()
        {
            return EmployeeV2.GetEmployees();
        }

        /// <summary>
        /// Get specific employee data by employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee data</returns>
        [HttpGet]
        public EmployeeV2 GetEmployee(int id)
        {
            return EmployeeV2.GetEmployees().FirstOrDefault(employee => employee.p01f01 == id);
        }

    }
}
