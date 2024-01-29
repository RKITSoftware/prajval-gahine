using JwtAuthProject.Authentication;
using JwtAuthProject.BL;
using JwtAuthProject.Models;
using System.Web.Http;

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
            EMP01 employee = BLEmployee.GetEmployee(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return BadRequest($"No employee found with employee id: {id}");

        }
    }
}
