using System;

namespace WebApplication2.Models
{
    /// <summary>
    /// Attendance class to store employee attendance
    /// </summary>
    public class ATD01
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
    }
}
