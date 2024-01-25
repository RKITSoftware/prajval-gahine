using FinalDemo.Models;
using FinalDemo.Service;
using System.Web.Http;

namespace FinalDemo.Controllers
{
    [RoutePrefix("api")]
    public class CLEmployeeController : ApiController
    {
        BLEmployeeService objEmployee = new BLEmployeeService();
        /// <summary>
        /// Gets the list of all employees.
        /// </summary>
        /// <returns>The list of employees or an error response if the operation fails.</returns>

        [HttpGet]
        [Route("GetEmployees")]
        public IHttpActionResult GetEmployee()
        {
            return Ok(objEmployee.GetEmployees());
        }
        /// <summary>
        /// Gets a specific employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The employee with the specified ID or an error response if the employee is not found.</returns>
        [HttpGet]
        [Route("GetEmployee-by-id/{id}")]
        public Emp01 GetEmployeeByID(int id)
        {
            return objEmployee.GetByID(id);
        }
        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="employee">The employee to add.</param>
        /// <returns>The added employee or an error response if the operation fails.</returns>

        [HttpPost]
        [Route("AddEmployee")]
        public IHttpActionResult AddEmployee(Emp01 objEmp)
        {
            // Check if the provided employee object is not null
            if (objEmp != null)
            {
                string Result = objEmployee.AddEmployee(objEmp);
                return Ok(Result);
            }
            else
            {
                // Return a BadRequest response if the input data is invalid
                return BadRequest("Please provide the required information");
            }
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="employee">The updated employee data.</param>
        /// <returns>The updated employee or an error response if the operation fails.</returns>

        [HttpPut]
        [Route("updateEmployee/{id}")]
        public IHttpActionResult UpdateEmployee(int id, Emp01 objEmp)
        {

            if (objEmp != null)
            {
                return Ok(objEmployee.UpdateEmployee(id, objEmp));
            }
            else
            {
                // Return BadRequest if the input data is invalid
                return BadRequest("Please provide the required information");
            }
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>The deleted employee or an error response if the operation fails.</returns>

        [HttpDelete]
        [Route("RemoveEmployee/{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            return Ok(objEmployee.RemoveEmployee(id));
        }
    }
}
