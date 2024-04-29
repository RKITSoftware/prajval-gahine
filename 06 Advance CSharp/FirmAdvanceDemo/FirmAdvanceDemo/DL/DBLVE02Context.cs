using FirmAdvanceDemo.Connection;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using static FirmAdvanceDemo.Utility.Constants;

namespace FirmAdvanceDemo.DB
{
    /// <summary>
    /// Provides methods for interacting with the LVE02 table in the database.
    /// </summary>
    public class DBLVE02Context
    {
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;

        private readonly string _baseSelectQuery = @"
                                    SELECT
                                        e02f01 AS E02101,
                                        e02f02 AS E02102,
                                        e02f03 AS E02103,
                                        e02f04 AS E02104,
                                        e02f05 AS E02105,
                                        e02f06 AS E02106,
                                        e02f07 AS E02107
                                    FROM
                                        lve02
                                    {0}";

        /// <summary>
        /// Initializes a new instance of the <see cref="DBLVE02Context"/> class.
        /// </summary>
        public DBLVE02Context(string baseSelectQuery = null)
        {
            _connection = MysqlDbConnector.Connection;
            _baseSelectQuery = baseSelectQuery;
        }

        /// <summary>
        /// Requests a leave based on the provided LVE02 object.
        /// </summary>
        /// <param name="objLVE02">The LVE02 object representing the leave request.</param>
        /// <returns>True if the leave request is successful, otherwise false.</returns>
        public bool RequestLeave(LVE02 objLVE02)
        {

            DateTime leaveEndDate = objLVE02.E02F03.AddDays(objLVE02.E02F04 - 1);

            string query = string.Format(@"
                                        SELECT
                                            COUNT(e02f01)
                                        FROM
                                            lve02
                                        WHERE
                                            e02f02 = {0} AND
                                            (
                                                ( '{1}' >= e02f03 AND '{1}' <= ADDDATE(e02f03, e02f04 - 1) ) OR
                                                ( '{2}' >= e02f03 AND '{2}' <= ADDDATE(e02f03, e02f04 - 1) ) OR
                                                ( '{1}' <= e02f04 AND '{2}' >= ADDDATE(e02f03, e02f04 - 1) )
                                            )",
                                            objLVE02.E02F02,
                                            objLVE02.E02F03.ToString(GlobalDateFormat),
                                            leaveEndDate.ToString(GlobalDateFormat));


            //string where = string.Format(@"
            //                        WHERE
            //                            (
            //                                (e02f03 >= '{0}' AND e02f03 <= '{1}') OR
            //                                (ADDDATE(e02f03, e02f04 - 1) >= '{0}' AND ADDDATE(e02f03, e02f04 - 1) <= '{1}') OR
            //                                (e02f03 < '{0}' AND ADDDATE(e02f03, e02f04 - 1) > '{1}')
            //                            )",
            //            startDate,
            //            endDate);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            _connection.Open();

            long leaveConflictCount;
            try
            {
                leaveConflictCount = (long)cmd.ExecuteScalar();
            }
            finally
            {
                _connection.Close();
            }

            return leaveConflictCount == 0;
        }

        /// <summary>
        /// Fetches all leave records.
        /// </summary>
        /// <returns>A DataTable containing all leave records.</returns>
        public DataTable FetchLeave()
        {
            DataTable dtLeave;

            string query = string.Format(_baseSelectQuery, string.Empty);

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, _connection);

            dtLeave = new DataTable();
            adapter.Fill(dtLeave);

            return dtLeave;
        }

        /// <summary>
        /// Fetches a specific leave record by its ID.
        /// </summary>
        /// <param name="leaveID">The ID of the leave record to fetch.</param>
        /// <returns>A DataTable containing the leave record.</returns>
        public DataTable FetchLeave(int leaveID)
        {
            DataTable dtLeave;

            string where = string.Format(@"
                        WHERE
                            e02f01 = {0}",
                            leaveID);

            string query = string.Format(_baseSelectQuery, where);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            dtLeave = new DataTable();
            adapter.Fill(dtLeave);

            return dtLeave;
        }

        /// <summary>
        /// Fetches leave records by their status.
        /// </summary>
        /// <param name="leaveStatus">The status of the leave records to fetch.</param>
        /// <returns>A DataTable containing the leave records with the specified status.</returns>
        public DataTable FetchLeaveByStatus(EnmLeaveStatus leaveStatus)
        {
            DataTable dtLeave;

            string where = string.Format(@"
                        WHERE
                            e02f06 = '{0}'",
                            leaveStatus);

            string query = string.Format(_baseSelectQuery, where);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            dtLeave = new DataTable();
            adapter.Fill(dtLeave);

            return dtLeave;
        }

        /// <summary>
        /// Fetches leave records for a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee whose leave records to fetch.</param>
        /// <returns>A DataTable containing the leave records for the specified employee.</returns>
        public DataTable FetchLeaveByEmployee(int employeeId)
        {
            DataTable dtLeave;

            string where = string.Format(@"
                        WHERE
                            e02f02 = {0}",
                            employeeId);

            string query = string.Format(_baseSelectQuery, where);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            dtLeave = new DataTable();
            adapter.Fill(dtLeave);

            return dtLeave;
        }

        /// <summary>
        /// Fetches leave records for a specific month and year.
        /// </summary>
        /// <param name="year">The year of the leave records.</param>
        /// <param name="month">The month of the leave records.</param>
        /// <returns>A DataTable containing the leave records for the specified month and year.</returns>
        public DataTable FetchLeaveByMonth(int year, int month)
        {
            DataTable dtLeave;

            string startDate = string.Format("{0}-{1}-01", year, month);
            string endDate = string.Format("{0}-{1}-{2}", year, month, DateTime.DaysInMonth(year, month));


            string where = string.Format(@"
                                    WHERE
                                        (
                                            (e02f03 >= '{0}' AND e02f03 <= '{1}') OR
                                            (ADDDATE(e02f03, e02f04 - 1) >= '{0}' AND ADDDATE(e02f03, e02f04 - 1) <= '{1}') OR
                                            (e02f03 < '{0}' AND ADDDATE(e02f03, e02f04 - 1) > '{1}')
                                        )",
                                    startDate,
                                    endDate);

            string query = string.Format(_baseSelectQuery, where);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            
            dtLeave = new DataTable();
            adapter.Fill(dtLeave);

            return dtLeave;
        }

        /// <summary>
        /// Fetches leave records for a specific date.
        /// </summary>
        /// <param name="date">The date of the leave records.</param>
        /// <returns>A DataTable containing the leave records for the specified date.</returns>
        public DataTable FetchLeaveForDate(DateTime date)
        {
            DataTable dtLeave;

            string where = string.Format(@"
                                    WHERE
                                        '{0}' >= DATE(e02f03) AND
                                        '{0}' <= ADDDATE(e02f03, e02f04 - 1)",
                                    date.ToString(Constants.GlobalDateFormat));

            string query = string.Format(_baseSelectQuery, where);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            dtLeave = new DataTable();
            adapter.Fill(dtLeave);

            return dtLeave;
        }

        /// <summary>
        /// Fetches leave records for a specific employee, month, and year.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <param name="year">The year of the leave records.</param>
        /// <param name="month">The month of the leave records.</param>
        /// <returns>A DataTable containing the leave records for the specified employee, month, and year.</returns>
        public DataTable FetchLeaveByEmployeeAndMonth(int employeeId, int year, int month)
        {
            DataTable dtLeave;
            string startDate = string.Format("{0}-{1}-01", year, month);
            string endDate = string.Format("{0}-{1}-{2}", year, month, DateTime.DaysInMonth(year, month));

            string where = string.Format(@"
                                    WHERE
                                        e02f02 = {0} AND
                                        (
                                            (e02f03 >= '{1}' AND e02f03 <= '{2}') OR
                                            (ADDDATE(e02f03, e02f04 - 1) >= '{1}' AND ADDDATE(e02f03, e02f04 - 1) <= '{2}') OR
                                            (e02f03 < '{1}' AND ADDDATE(e02f03, e02f04 - 1) > '{2}')
                                        )",
                                    employeeId,
                                    startDate,
                                    endDate);

            string query = string.Format(_baseSelectQuery, where);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                dtLeave = new DataTable();
                adapter.Fill(dtLeave);
            }
            finally
            {
                _connection.Close();
            }
            return dtLeave;
        }

        /// <summary>
        /// Fetches leave records for a specific employee and year.
        /// </summary>
        /// <param name="employeeID">The ID of the employee.</param>
        /// <param name="year">The year of the leave records.</param>
        /// <returns>A DataTable containing the leave records for the specified employee and year.</returns>
        public DataTable FetchLeaveByEmployeeAndYear(int employeeID, int year)
        {
            DataTable dtLeave;

            string startDate = string.Format("{0}-01-01", year);
            string endDate = string.Format("{0}-12-31", year);

            string where = string.Format(@"
                                    WHERE
                                        e01f02 = {0} AND
                                        (
                                            (e02f03 >= '{1}' AND e02f03 <= '{2}') OR
                                            (ADDDATE(e02f03, e02f04 - 1) >= '{1}' AND ADDDATE(e02f03, e02f04 - 1) <= '{2}') OR
                                            (e02f03 < '{1}' AND ADDDATE(e02f03, e02f04 - 1) > '{2}')
                                        )",
                                        employeeID,
                                        startDate,
                                        endDate);

            string query = string.Format(_baseSelectQuery, where);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                dtLeave = new DataTable();
                adapter.Fill(dtLeave);
            }
            finally
            {
                _connection.Close();
            }
            return dtLeave;
        }

        public DataTable FetchLeaveGeneral(int employeeID, int year = 0, int month = 0, int day = 0)
        {
            string employeeIDWhere = string.Empty;
            string dateWhere = string.Empty;

            if (employeeID != 0)
            {
                employeeIDWhere = string.Format(@"
                                    e02f02 = {0}
                                    ",
                                    employeeID);
            }

            if (day != 0)
            {
                DateTime date = new DateTime(year, month, day);
                dateWhere = string.Format(@"
                                    ( '{0}' >= e02f03 AND '{0}' <= ADDDATE(e02f03, e02f04 - 1) )",
                                    date.ToString(Constants.GlobalDateFormat));
            }
            else if (year != 0)
            {
                string startDate;
                string endDate;
                if (month != 0)
                {
                    startDate = string.Format("{0}-{1}-01", year, month);
                    endDate = string.Format("{0}-{1}-{2}", year, month, DateTime.DaysInMonth(year, month));
                }
                else
                {
                    startDate = string.Format("{0}-01-01", year);
                    endDate = string.Format("{0}-12-31", year);
                }


                dateWhere = string.Format(@"
                                    (
                                            (e02f03 >= '{0}' AND e02f03 <= '{1}') OR
                                            (ADDDATE(e02f03, e02f04 - 1) >= '{0}' AND ADDDATE(e02f03, e02f04 - 1) <= '{1}') OR
                                            (e02f03 < '{0}' AND ADDDATE(e02f03, e02f04 - 1) > '{1}')
                                    )",
                                    startDate,
                                    endDate);
            }

            DataTable dtLeave;

            string where = string.Format("WHERE {0} AND {1}", employeeIDWhere, dateWhere);

            string query = string.Format(_baseSelectQuery, where);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            
            dtLeave = new DataTable();
            adapter.Fill(dtLeave);

            return dtLeave;
        }
    }
}