using ExceptionInWebAPIProject.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExceptionInWebAPIProject.Controllers
{
    /// <summary>
    /// Class to handle request for controller = employee
    /// </summary>
    public class EmployeeController : ApiController
    {
        /// <summary>
        /// Method to Get employee info with specified employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employe info</returns>
        /// <exception cref="HttpResponseException"></exception>
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

        /// <summary>
        /// Method to Get EmployeeSalary with specified employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <exception cref="NotImplementedException">By default method is designed to throw NoImplementedException object</exception>

        [HttpGet]
        [Route("api/employee/salary/{id}")]
        [NotImplExceptionAttribute]
        public void GetEmployeeSalary(int id)
        {
            // throwing NotImplementedException b/c currently no implementation is defined for this api/endpoint
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method to get employee attendances of employee with specified employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns> Action result containing list of attendance-date or null </returns>
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
