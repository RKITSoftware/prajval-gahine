using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VersioningDemo.Models
{
    public class EmployeeV1
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
        /// Returns employees for web api version v1
        /// </summary>
        /// <returns>list of employees (EmployeeV1)</returns>
        public static List<EmployeeV1> GetEmployees()
        {
            return new List<EmployeeV1>()
            {
                new EmployeeV1 { p01f01 = 101, p01f02 = "prajval"},
                new EmployeeV1 { p01f01 = 102, p01f02 = "yash"},
                new EmployeeV1 { p01f01 = 103, p01f02 = "deep"},
                new EmployeeV1 { p01f01 = 104, p01f02 = "krinsi"}
            };
        }
    }
}