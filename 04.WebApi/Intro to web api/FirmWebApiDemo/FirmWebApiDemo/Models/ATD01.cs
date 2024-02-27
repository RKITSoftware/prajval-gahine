using System;

namespace FirmWebApiDemo.Models
{
    public class ATD01
    {
        /// <summary>
        /// Attendance id
        /// </summary>
        public int d01f01;

        /// <summary>
        /// Employee id
        /// </summary>
        public int d01f02;

        /// <summary>
        /// Employee Date of attendance
        /// </summary>
        public DateTime d01f03;

        /// <summary>
        /// Attendance model
        /// </summary>
        /// <param name="AttendanceId">Attendance Id</param>
        /// <param name="EmployeeId">Employee Id</param>
        /// <param name="AttendanceDate">Attendance Date</param>
        public ATD01(int AttendanceId, int EmployeeId, DateTime AttendanceDate)
        {
            this.d01f01 = AttendanceId;
            this.d01f02 = EmployeeId;
            this.d01f03 = AttendanceDate;
        }
    }
}