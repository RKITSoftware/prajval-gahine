using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FirmWebApiDemo.Models
{
    public class ATD01
    {
        /// <summary>
        /// File location to Attendance.json file
        /// </summary>
        private static string AttendacneFilePath = HttpContext.Current.Server.MapPath(@"~/data/Attendance.json");

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

        public ATD01(int AttendanceId, int EmployeeId, DateTime AttendanceDate)
        {
            this.d01f01 = AttendanceId;
            this.d01f02 = EmployeeId;
            this.d01f03 = AttendanceDate;
        }

        /// <summary>
        /// Gets all Employee Attedance list
        /// </summary>
        /// <returns>list of attendances</returns>
        public static List<ATD01> GetAttendances()
        {
            List<ATD01> lstAttendance = null;

            using (StreamReader sr = new StreamReader(AttendacneFilePath))
            {
                string attendanceJson = sr.ReadToEnd();
                lstAttendance = JsonConvert.DeserializeObject<List<ATD01>>(attendanceJson);
            }

            return lstAttendance;
        }

        /// <summary>
        /// Method to add attendance in Attendance.json file array
        /// </summary>
        /// <param name="attendance">An Attendance object of ATD01 type</param>
        public static void SetAttendance(ATD01 attendance)
        {
            List<ATD01> lstAttendance = GetAttendances();

            // add the user to list
            lstAttendance.Add(attendance);

            using (StreamWriter sw = new StreamWriter(AttendacneFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(lstAttendance, Formatting.Indented));
            }
        }

        /// <summary>
        /// Method to add list of attendance in Attendance.json file array
        /// </summary>
        /// <param name="employee">An Attendance List of ATD01 type</param>
        public static void SetAttendances(List<ATD01> lstAttendance)
        {
            using (StreamWriter sw = new StreamWriter(AttendacneFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(lstAttendance, Formatting.Indented));
            }
        }
    }
}