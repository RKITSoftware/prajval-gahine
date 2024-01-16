using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VersioningDemo.Models;

namespace VersioningDemo.Controllers
{
    //[RoutePrefix("api/v1/employee")]
    //[RoutePrefix("api/employee")]
    /// <summary>
    /// Employee controller of version v1
    /// </summary>
    public class EmployeeV1Controller : ApiController
    {

        /// <summary>
        /// Get employee list
        /// </summary>
        /// <returns>list of employee</returns>
        [HttpGet]
        //[Route("getemployees")]
        public List<EmployeeV1> GetEmployees()
        {
            return EmployeeV1.GetEmployees();
        }

        /// <summary>
        /// Get specific employee detail using employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee data</returns>
        [HttpGet]
        //[Route("{id}")]
        public EmployeeV1 GetEmployee(int id)
        {
            return EmployeeV1.GetEmployees().FirstOrDefault(employee => employee.p01f01 == id);
        }

        // doubt
        //[HttpGet]
        //[Route("{id}")]
        //public string GetAddress(int id)
        //{
        //    return "get address";
        //}
    }
}
