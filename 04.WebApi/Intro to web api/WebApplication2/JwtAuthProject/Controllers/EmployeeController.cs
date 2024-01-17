using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JwtAuthProject.Authentication;
using JwtAuthProject.Models;

namespace JwtAuthProject.Controllers
{
    /// <summary>
    /// Employee controller to handle employee specific requests
    /// </summary>
    public class EmployeeController : ApiController
    {
        /// <summary>
        /// Get a specific employee based on employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>An instance of EMP01 based on emlpoyee id</returns>
        [HttpGet]
        [Route("api/employee/{id}")]
        [BearerAuthentication]
        public IHttpActionResult GetEmployee(int id)
        {
            EMP01 employee = EMP01.GetEmployee(id);
            if(employee != null)
            {
                return Ok(employee);
            }
            return BadRequest($"No employee found with employee id: {id}");

        }
    }
}
