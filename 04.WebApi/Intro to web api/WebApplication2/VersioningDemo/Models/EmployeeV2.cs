using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VersioningDemo.Models
{
    public class EmployeeV2
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
        /// Employee joining date
        /// </summary>
        public DateTime p01f03 { get; set; }

        /// <summary>
        /// Returns employees for web api version v1
        /// </summary>
        /// <returns>list of employees (EmployeeV1)</returns>
        public static List<EmployeeV2> GetEmployees()
        {
            return new List<EmployeeV2>()
            {
                new EmployeeV2 { p01f01 = 101, p01f02 = "prajval", p01f03 = new DateTime(2024, 1, 8)},
                new EmployeeV2 { p01f01 = 102, p01f02 = "yash", p01f03 = new DateTime(2024, 1, 8)},
                new EmployeeV2 { p01f01 = 103, p01f02 = "deep", p01f03 = new DateTime(2024, 1, 8)},
                new EmployeeV2 { p01f01 = 104, p01f02 = "krinsi", p01f03 = new DateTime(2024, 1, 8)}
            };
        }
    }
}