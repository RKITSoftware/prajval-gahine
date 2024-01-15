using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VersioningDemo.Models;

namespace VersioningDemo.Controllers
{

    //[RoutePrefix("api/v2/employee")]
    public class EmployeeV2Controller : ApiController
    {
        [HttpGet]
        //[Route("getemployees")]
        public List<EmployeeV2> GetEmployees()
        {
            return EmployeeV2.GetEmployees();
        }
        [HttpGet]
        public EmployeeV2 GetEmployee(int id)
        {
            return EmployeeV2.GetEmployees().FirstOrDefault(employee => employee.p01f01 == id);
        }

    }
}
