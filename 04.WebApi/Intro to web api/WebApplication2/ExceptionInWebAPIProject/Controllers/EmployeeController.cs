using ExceptionInWebAPIProject.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExceptionInWebAPIProject.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("api/employee/{id}")]
        public IHttpActionResult GetEmployee(int id)
        {
            // let say there donot exists such employee with employeeId id
            string employee;
            if (id == 101)
            {
                employee= null;
            }
            else
            {
                employee = "prajval";
            }

            if(employee == null)
            {
                // constructor overload that takes HttpStatusCode
                // throw new HttpResponseException(HttpStatusCode.NotFound);

                // another constructor oevrload
                HttpResponseMessage reponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No employee exists with employee id: {id}"),
                    ReasonPhrase = "Employee id not found"
                };

                throw new HttpResponseException(reponseMessage);
            }
            return Ok(employee);
        }

        [HttpGet]
        [Route("api/employee/salary/{id}")]
        [NotImplExceptionAttribute]
        public void GetEmployeeSalary(int id)
        {
            // throwing NotImplementedException b/c currently no implementation is defined for this api/endpoint
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("api/employee/attendance/{id}")]
        public HttpResponseMessage GetEmployeeAttendance(int id)
        {
            if(id == 101)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id 101 does not have attendance data");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new DateTime[]
                {
                    new DateTime(2024, 1, 1),
                    new DateTime(2024, 2, 1),
                    new DateTime(2024, 3, 1)
                });
            }
        }
    }
}
