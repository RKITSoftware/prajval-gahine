using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FirmWebApiDemo.Models
{
    public class EMP01
    {        
        /// <summary>
        /// File location to Employee.json file
        /// </summary>
        private static string EmployeeFilePath = HttpContext.Current.Server.MapPath(@"~/data/Employee.json");

        /// <summary>
        /// Employee id
        /// </summary>
        public int p01f01 { get; set; }

        /// <summary>
        /// Employee first name
        /// </summary>
        public string p01f02 { get; set; } 

        /// <summary>
        /// Employee last name
        /// </summary>
        public string p01f03 { get; set; }

        /// <summary>
        /// Employee gender
        /// </summary>
        public string p01f04 { get; set; }

        /// <summary>
        /// Employee Date of birth
        /// </summary>
        public DateTime p01f05 { get; set; }

        // get all employees
        /// <summary>
        /// Gets all employees list
        /// </summary>
        /// <returns>list of employees</returns>
        public static List<EMP01> GetEmployees()
        {
            List<EMP01> employees = null;

            using (StreamReader sr = new StreamReader(EmployeeFilePath))
            {
                string employeeJson = sr.ReadToEnd();
                employees = JsonConvert.DeserializeObject<List<EMP01>>(employeeJson);
            }

            return employees;
        }

        /// <summary>
        /// Method to add employee in Employee.json file array
        /// </summary>
        /// <param name="user">An employee object of EMP01 type</param>
        public static void SetEmployee(EMP01 employee)
        {
            List<EMP01> employees =  GetEmployees();

            // add the user to list
            employees.Add(employee);

            using (StreamWriter sw = new StreamWriter(EmployeeFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(employees, Formatting.Indented));
            }
        }

        /// <summary>
        /// Method to add employees in Employee.json file array
        /// </summary>
        /// <param name="employee">An Employee List of EMP01 type</param>
        public static void SetEmployees(List<EMP01> lstEmployee)
        {
            using (StreamWriter sw = new StreamWriter(EmployeeFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(lstEmployee, Formatting.Indented));
            }
        }

    }
}