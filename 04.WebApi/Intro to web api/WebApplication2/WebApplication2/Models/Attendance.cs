using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Models
{
    public class Attendance
    {
        /// <summary>
        /// Employee attendance id
        /// </summary>
        public int d01f01 { get; set; }

        /// <summary>
        /// Employee id for whom the attendance is intended (or filled)
        /// </summary>
        public int d01f02 { get; set; }

        /// <summary>
        /// Date of attendance
        /// </summary>

        public DateTime d01f03 { get; set; }

        public static List<Attendance> GetAttendances()
        {
            List<Attendance> attendances = new List<Attendance>()
            {
                new Attendance {d01f01 = 1, d01f02 = 1, d01f03 = new DateTime(2024, 1, 1)},
                new Attendance {d01f01 = 2, d01f02 = 2, d01f03 = new DateTime(2024, 1, 1)},
                new Attendance {d01f01 = 3, d01f02 = 3, d01f03 = new DateTime(2024, 1, 1)},
                new Attendance {d01f01 = 4, d01f02 = 1, d01f03 = new DateTime(2024, 1, 2)},
                new Attendance {d01f01 = 5, d01f02 = 2, d01f03 = new DateTime(2024, 1, 2)},
                new Attendance {d01f01 = 6, d01f02 = 3, d01f03 = new DateTime(2024, 1, 3)}

            };

            return attendances;
        }
    }
}
