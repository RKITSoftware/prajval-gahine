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
    [RoutePrefix("api/employee")]
    public class EmployeeV1Controller : ApiController
    {


        [HttpGet]
        //[Route("getemployees")]
        public List<EmployeeV1> GetEmployees()
        {
            return EmployeeV1.GetEmployees();
        }

        [HttpGet]
        [Route("{id}")]
        public EmployeeV1 GetEmployee(int id)
        {
            return EmployeeV1.GetEmployees().FirstOrDefault(employee => employee.p01f01 == id);
        }
    }
}
