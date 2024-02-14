
using JwtAuthProject.Authentication;
using JwtAuthProject.Models;
using System.Collections.Generic;

namespace JwtAuthProject.BL
{
    /// <summary>
    /// Employee Buisness Logic class to provide employee related functionalities
    /// </summary>
    public class BLEmployee
    {
        /// <summary>
        /// Method to return list of employee of EMP01 class
        /// </summary>
        /// <returns>list of employees</returns>
        public static List<EMP01> GetEmployees()
        {
            return new List<EMP01>
            {
                new EMP01 (1, "prajval gahine"),
                new EMP01 (2, "yash lathiya"),
                new EMP01 (3, "deep patel"),
                new EMP01 (4, "krinsi kayada")
            };
        }

        /// <summary>
        /// Get specific employee based on employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>An instance of EMP01 or null if not employee found with employeeId id</returns>
        public static EMP01 GetEmployee(int id)
        {
            List<EMP01> lstEmployee = BLEmployee.GetEmployees();
            int index = lstEmployee.FindIndex(employee => employee.p01f01 == id);
            if (index != -1)
            {
                return lstEmployee[index];
            }
            return null;
        }
    }
}