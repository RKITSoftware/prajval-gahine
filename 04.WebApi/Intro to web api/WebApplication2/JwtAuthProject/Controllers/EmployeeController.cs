using JwtAuthProject.Authentication;
using JwtAuthProject.BL;
using JwtAuthProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace JwtAuthProject.Controllers
{
    /// <summary>
    /// Employee controller to handle employee specific requests
    /// </summary>
    [BearerAuthentication]
    public class EmployeeController : ApiController
    {
        /// <summary>
        /// Get a specific employee based on employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>An instance of EMP01 based on emlpoyee id</returns>
        [HttpGet]
        [Route("api/employee")]
        [BasicAuthorization(Roles = "employee")]
        public IHttpActionResult GetEmployee()
        {
            //Get the current claims principal
            ClaimsPrincipal identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            // Get the claims 
            string userId = ((IEnumerable<Claim>)identity.Claims).Where(c => c.Type == "r01f01")
                               .Select(c => c.Value).SingleOrDefault();

            EMP01 employee = BLEmployee.GetEmployee(int.Parse(userId));
            if (employee != null)
            {
                return Ok(employee);
            }
            return BadRequest($"No employee found with employee id: {userId}");
        }

        /// <summary>
        /// Get all employee data
        /// </summary>
        /// <returns>List of employees</returns>
        [HttpGet]
        [Route("api/employees")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetEmployees()
        {
            List<EMP01> lstEmployee = BLEmployee.GetEmployees();
            if (lstEmployee != null)
            {
                return Ok(lstEmployee);
            }
            return BadRequest($"No employee found");
        }
    }
}
