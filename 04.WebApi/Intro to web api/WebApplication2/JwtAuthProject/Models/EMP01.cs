using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JwtAuthProject.Models
{
    public class EMP01
    {
        /// <summary>
        /// Employee id
        /// </summary>
        public int p01f01 { get; set; }

        /// <summary>
        /// Employee name
        /// </summary>
        public string p01f02 { get; set; }


        /// <summary>
        /// Constructor to create EMP01 instance with employee id and name
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <param name="name">Employee name</param>
        public EMP01(int p01f01, string p01f02)
        {
            this.p01f01 = p01f01;
            this.p01f02 = p01f02;
        }

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
            List<EMP01> lstEmployee = EMP01.GetEmployees();
            int index = lstEmployee.FindIndex(employee => employee.p01f01 == id);
            if(index != -1)
            {
                return lstEmployee[index];
            }
            return null;
        }
    }
}