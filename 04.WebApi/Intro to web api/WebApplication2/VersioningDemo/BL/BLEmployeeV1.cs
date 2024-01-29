using System.Collections.Generic;
using VersioningDemo.Models;

namespace VersioningDemo.BL
{
    public class BLEmployeeV1
    {
        /// <summary>
        /// Returns employees for web api version v1
        /// </summary>
        /// <returns>list of employees (EMP01V2 returns>
        public static List<EMP01V1> GetEmployees()
        {
            return new List<EMP01V1>()
            {
                new EMP01V1 { p01f01 = 101, p01f02 = "prajval"},
                new EMP01V1 { p01f01 = 102, p01f02 = "yash"},
                new EMP01V1 { p01f01 = 103, p01f02 = "deep"},
                new EMP01V1 { p01f01 = 104, p01f02 = "krinsi"}
            };
        }
    }
}