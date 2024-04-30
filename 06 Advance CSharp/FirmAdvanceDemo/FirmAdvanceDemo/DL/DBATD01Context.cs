using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using static FirmAdvanceDemo.Utility.Constants;

namespace FirmAdvanceDemo.DB
{
    /// <summary>
    /// Provides methods for interacting with the ATD01 table in the database.
    /// </summary>
    public class DBATD01Context
    {
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the DBATD01Context class.
        /// </summary>
        public DBATD01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        /// <summary>
        /// Fetches attendance records for a specified year and month.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns>A DataTable containing the attendance records.</returns>
        public DataTable FetchAttendanceByMonthYear(int year, int month)
        {
            DataTable dtAttendance;
            string query = string.Format(
                            @"
                                SELECT
                                    d01f01,
                                    d01f02,
                                    d01f03,
                                    d01f04
                                FROM
                                    atd01
                                WHERE
                                    YEAR(d01f03) = {0} AND
                                    MONTH(d01f03) = {1}",
                            year,
                            month);
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            dtAttendance = new DataTable();
            adapter.Fill(dtAttendance);

            return dtAttendance;
        }

        /// <summary>
        /// Fetches attendance records for today's date.
        /// </summary>
        /// <returns>A DataTable containing the attendance records.</returns>
        public DataTable FetchAttendanceForToday()
        {
            DataTable dtAttendance;
            DateTime now = DateTime.Now;

            string query = string.Format(
                            @"
                                SELECT
                                    d01f01,
                                    d01f02,
                                    d01f03,
                                    d01f04
                                FROM
                                    atd01
                                WHERE
                                    DATE(d01f03) = {0}",
                            now.ToString(GlobalDateFormat));

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            dtAttendance = new DataTable();
            adapter.Fill(dtAttendance);

            return dtAttendance;
        }

        /// <summary>
        /// Fetches attendance records for a specified employee ID, year, and month.
        /// </summary>
        /// <param name="employeeID">The employee ID.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns>A DataTable containing the attendance records.</returns>
        public DataTable FetchAttendanceByemployeeIDAndMonthYear(int employeeID, int year, int month)
        {
            DataTable dtAttendance;

            string query = string.Format(
                            @"
                                SELECT
                                    d01f01,
                                    d01f02,
                                    d01f03,
                                    d01f04
                                FROM
                                    atd01
                                WHERE
                                    d01f02 = {0} AND
                                    YEAR(d01f03) = {1} AND
                                    MONTH(d01f03) = {2}",
                            employeeID,
                            year,
                            month);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            dtAttendance = new DataTable();
            adapter.Fill(dtAttendance);

            return dtAttendance;
        }


        /// <summary>
        /// Retrieves attendance records for a specified employee ID.
        /// </summary>
        /// <param name="employeeID">The employee ID.</param>
        /// <returns>A DataTable containing the attendance records.</returns>
        public DataTable RetrieveAttendanceByemployeeID(int employeeID)
        {
            DataTable dtAttendance;

            string query = string.Format(
                            @"
                                SELECT
                                    d01f01,
                                    d01f02,
                                    d01f03,
                                    d01f04
                                FROM
                                    atd01
                                WHERE
                                    d01f02 = {0}",
                            employeeID);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            dtAttendance = new DataTable();
            adapter.Fill(dtAttendance);

            return dtAttendance;
        }

        /// <summary>
        /// Fetches all attendance records.
        /// </summary>
        /// <returns>A DataTable containing all attendance records.</returns>
        public DataTable FetchAttendance()
        {
            DataTable dtAttendance = new DataTable();

            string query = @"SELECT
                                d01f01 AS D01101,
                                d01f02 AS D01102,
                                d01f03 AS D01103,
                                d01f04 AS D01104
                            FROM atd01";

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);


            adapter.Fill(dtAttendance);

            return dtAttendance;
        }

        /// <summary>
        /// Fetches the attendance record with the specified attendance ID.
        /// </summary>
        /// <param name="attendanceID">The attendance ID.</param>
        /// <returns>A DataTable containing the attendance record.</returns>
        public DataTable FetchAttendance(int attendanceID)
        {
            DataTable dtAttendance = new DataTable();

            string query = string.Format(
                            @"SELECT
                                d01f01 AS D01101, 
                                d01f02 AS D01102,
                                d01f03 AS D01103,
                                d01f04 AS D01104
                            FROM atd01
                                WHERE d01f01 = {0};", attendanceID);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dtAttendance);

            return dtAttendance;
        }
    }
}