using System;
using System.Collections.Generic;
using WebApplication2.Models;

namespace WebApplication2.BL
{
    /// <summary>
    /// Attendance Business Logic class to manage employee attendance
    /// </summary>
    public class BLAttendance
    {
        /// <summary>
        /// Method to return list of attendance of employee
        /// </summary>
        /// <returns></returns>
        public static List<ATD01> GetAttendances()
        {
            List<ATD01> lstAttendance = new List<ATD01>()
            {
                new ATD01 {d01f01 = 1, d01f02 = 1, d01f03 = new DateTime(2024, 1, 1)},
                new ATD01 {d01f01 = 2, d01f02 = 2, d01f03 = new DateTime(2024, 1, 1)},
                new ATD01 {d01f01 = 3, d01f02 = 3, d01f03 = new DateTime(2024, 1, 1)},
                new ATD01 {d01f01 = 4, d01f02 = 1, d01f03 = new DateTime(2024, 1, 2)},
                new ATD01 {d01f01 = 5, d01f02 = 2, d01f03 = new DateTime(2024, 1, 2)},
                new ATD01 {d01f01 = 6, d01f02 = 3, d01f03 = new DateTime(2024, 1, 3)}
            };

            return lstAttendance;
        }
    }
}