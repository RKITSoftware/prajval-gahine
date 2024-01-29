using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VersioningDemo.BL;
using VersioningDemo.Models;

namespace VersioningDemo.Controllers
{
    /// <summary>
    /// Employee controller of version v2
    /// </summary>
    public class EmployeeV2Controller : ApiController
    {
        /// <summary>
        /// Get employee list
        /// </summary>
        /// <returns>list of employee</returns>
        [HttpGet]
        //[Route("getemployees")]
        public List<EMP01V2> GetEmployees()
        {
            return BLEmployeeV2.GetEmployees();
        }

        /// <summary>
        /// Get specific employee data by employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee data</returns>
        [HttpGet]
        public EMP01V2 GetEmployee(int id)
        {
            return BLEmployeeV2.GetEmployees().FirstOrDefault(employee => employee.p01f01 == id);
        }
    }
}
