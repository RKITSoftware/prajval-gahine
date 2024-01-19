using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.ModelBinding;
using WebApplication2.Models;
using WebApplication2.Authentication;
using Newtonsoft.Json.Serialization;

namespace WebApplication2.Controllers
{
    [BasicAuthenticationAttribute]
    public class CLEmployeeController : ApiController
    {
        /// <summary>
        /// contains list of employee
        /// </summary>
        public static List<EMP01> lstEmployee;

        /// <summary>
        /// to remember the next free employee id
        /// </summary>
        public static int NextEmplopyeeId;

        /// <summary>
        /// static constructor to instialize static fields of CLEmployeeController class
        /// </summary>
        static CLEmployeeController()
        {
            NextEmplopyeeId = 101;
            lstEmployee = new List<EMP01>()
            {
                new EMP01( NextEmplopyeeId++, "Prajval"),
                new EMP01( NextEmplopyeeId++, "Yash")
            };
        }

        /// <summary>
        /// used to get all employee list
        /// </summary>
        /// <returns></returns>
        //[DisableCors()]
        //[EnableCors(origins: "https://www.google.com", headers: "name", methods: "*", exposedHeaders: "surname: gahine")]
        //[EnableCors(origins: "https://www.google.com", headers: "authorization,surname", methods: "*", exposedHeaders: "surname: gahine")]
        [EnableCors(origins: "https://www.google.com", headers: "authorization,accept,content-type,origin,x-my-header,deep", methods: "*", exposedHeaders: "name")]
        [HttpGet]
        [Route("api/employees")]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        public HttpResponseMessage GetEmployee()
        {
            var resp = new HttpResponseMessage()
            {
                Content = new StringContent("list of employees")
            };
            resp.Headers.Add("name", "prajval");
            return resp;
        }
        //fetch("http://localhost:51414/api/employees",{
        //headers:
        //    {
        //    Authorization: "Basic cHJpdGVzaGdhaGluZTpwcml0ZXNoQDEyMw==",
        //      accept: "application/json",
        //      "content-type": "application/json",
        //      origin: "https://www.google.com",
        //}
        //}).then(resp => {
        //    console.log("name: " + resp.headers.get("name"));
        //});

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEmployeeWithoutId">json object that contains employee data (without employee id)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/employee")]
        public IHttpActionResult PostEmployee(JObject newEmployeeWithoutId)
        {
            string name = newEmployeeWithoutId["p01f02"].ToString();
            EMP01 newEmployeeWithId = new EMP01(NextEmplopyeeId++, name);
            lstEmployee.Add(newEmployeeWithId);
            return Ok(newEmployeeWithId);
        }

        /// <summary>
        /// controller function to put (replace) whole employee object (employeeId remains same)
        /// </summary>
        /// <param name="updatedEmployee">new employee object to update</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/employee")]
        public IHttpActionResult PutEmployee(EMP01 updatedEmployee)
        {
            // return employee not found in case employeeId is >= nextEmployeeId
            if (updatedEmployee.p01f01 >= NextEmplopyeeId)
            {
                return Content(HttpStatusCode.NotFound, $"No employee exists with employeeId: {updatedEmployee.p01f01}");
            }

            // return employee not found error if employeeId is not present in the employee list
            int emnployeeIndex = lstEmployee.FindIndex(employee => employee.p01f01 == updatedEmployee.p01f01);
            if (emnployeeIndex == -1)
            {
                return Content(HttpStatusCode.NotFound, $"No employee exists with employeeId: {updatedEmployee.p01f01}");
            }

            // else update the employee object in the employee list
            lstEmployee[emnployeeIndex] = updatedEmployee;

            return Ok(updatedEmployee);
        }

        /// <summary>
        /// controller function to update some portion of employee object in employee list
        /// </summary>
        /// <param name="id">employee id to patch</param>
        /// <param name="employeeData">employee data in json to update</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("api/employee/{id}")]
        [BasicAuthorizationAttribute(Roles = "Admin,Employee")]
        public IHttpActionResult PatchEmployee(int id, [FromBody]JObject employeeData)
        {
            // return employee not found in case employeeId is >= nextEmployeeId
            if (id >= NextEmplopyeeId)
            {
                return Content(HttpStatusCode.NotFound, $"No employee exists with employeeId: {id}");
            }

            // return employee not found error if employeeId is not present in the employee list
            int emnployeeIndex = lstEmployee.FindIndex(employee => employee.p01f01 == id);
            if (emnployeeIndex == -1)
            {
                return Content(HttpStatusCode.NotFound, $"No employee exists with employeeId: {id}");
            }

            // employee exits
            EMP01 existingEmployee = lstEmployee[emnployeeIndex];
            foreach(PropertyInfo prop in existingEmployee.GetType().GetProperties())
            {
                foreach(JProperty prop2 in employeeData.Properties())
                {
                    if (prop.Name != "p01f01" && prop.Name == prop2.Name)
                    {
                        prop.SetValue(existingEmployee, prop2.Value.ToString());
                    }
                }
            }

            return Ok();
        }

        /// <summary>
        /// controller function to delete an employee resource from employee list
        /// </summary>
        /// <param name="EmployeeIdToDelete">Employee id to delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/employee/{EmployeeIdToDelete}")]
        public IHttpActionResult DeleteEmployee(int EmployeeIdToDelete)
        {
            // check if the employee id is out of employee id range
            if (EmployeeIdToDelete >= NextEmplopyeeId)
            {
                return Content(HttpStatusCode.NotFound, $"No employee exists with employeeId: {EmployeeIdToDelete} to delete");
            }

            int EmployeeToDeleteIndex = lstEmployee.FindIndex(employee => employee.p01f01 == EmployeeIdToDelete);

            // check if employee id does not exists
            if (EmployeeToDeleteIndex == -1)
            {
                return Content(HttpStatusCode.NotFound, $"No employee exists with employeeId: {EmployeeIdToDelete} to delete");
            }

            // employee exists => so delete the employee
            lstEmployee.RemoveAt(EmployeeToDeleteIndex);

            return Ok($"Employee with employee id: {EmployeeIdToDelete} remove successfully");
        }
    }
}
