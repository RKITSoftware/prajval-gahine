using ConsoleToWebAPIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConsoleToWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// GetAllEmployees - method returns all the employee object
        /// </summary>
        /// <returns> EmployeeModel object list (array)</returns>
        [Route("")]
        public List<EmployeeModel> GetAllEmployees()
        {
            // Instead of List u may use IEnumerable or IAsyncEnumerable
            return new List<EmployeeModel>()
            {
                new EmployeeModel(){ id = 10, name = "Prajval" },
                new EmployeeModel(){ id = 20, name = "Rahul" }
            };
        }

        /// <summary>
        /// GetEmployee - this action method returns the data of specified employee using id varibale
        /// </summary>
        /// <param name="id"> variable that uniquely identifies an employee</param>
        /// <returns> returns an EmployeeModel object wrapped under IActionResult type if id variable value is valid or returns NotFound type </returns>
        [Route("{id}")] 
        public IActionResult GetEmployee(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            return Ok(new EmployeeModel() { id = id, name = "Prajval" });
        }


        /// <summary>
        /// GetEmployeeBasicDetail - same as GetEmployee action method, here it is just to simulate ActionResult type
        /// </summary>
        /// <param name="id"> variable that uniquely identifies an employee</param>
        /// <returns> returns an EmployeeModel object if id variable value is valid or returns NotFound type</returns>
        [Route("{id}/basic")]
        public ActionResult<EmployeeModel> GetEmployeeBasicDetail(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            return new EmployeeModel() { id = id, name = "Prajval" };
        }
    }
}
