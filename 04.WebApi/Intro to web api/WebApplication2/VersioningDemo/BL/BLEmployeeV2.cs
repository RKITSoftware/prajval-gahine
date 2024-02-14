using System;
using System.Collections.Generic;
using VersioningDemo.Models;

namespace VersioningDemo.BL
{
    public class BLEmployeeV2
    {

        /// <summary>
        /// Returns employees for web api version v1
        /// </summary>
        /// <returns>list of employees (EMP01V2)</returns>
        public static List<EMP01V2> GetEmployees()
        {
            return new List<EMP01V2>()
            {
                new EMP01V2 { p01f01 = 101, p01f02 = "prajval", p01f03 = new DateTime(2024, 1, 8)},
                new EMP01V2 { p01f01 = 102, p01f02 = "yash", p01f03 = new DateTime(2024, 1, 8)},
                new EMP01V2 { p01f01 = 103, p01f02 = "deep", p01f03 = new DateTime(2024, 1, 8)},
                new EMP01V2 { p01f01 = 104, p01f02 = "krinsi", p01f03 = new DateTime(2024, 1, 8)}
            };
        }
    }
}