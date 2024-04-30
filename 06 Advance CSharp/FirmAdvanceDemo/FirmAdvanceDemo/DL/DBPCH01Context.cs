using FirmAdvanceDemo.Connection;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FirmAdvanceDemo.DB
{
    /// <summary>
    /// Provides methods for interacting with the PCH01 table in the database.
    /// </summary>
    public class DBPCH01Context
    {
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBPCH01Context"/> class.
        /// </summary>
        public DBPCH01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        /// <summary>
        /// Fetches all punch records.
        /// </summary>
        /// <returns>A DataTable containing all punch records.</returns>
        public DataTable FetchPunch()
        {
            DataTable dtPunch = new DataTable();

            string query = @"SELECT
                                h01f01 AS H01101,
                                h01f02 AS H01102,
                                h01f03 AS H01103,
                                h01f04 AS H01104
                            FROM pch01;";

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dtPunch);

            return dtPunch;
        }

        /// <summary>
        /// Fetches a specific punch record by its ID.
        /// </summary>
        /// <param name="punchId">The ID of the punch record to fetch.</param>
        /// <returns>A DataTable containing the punch record with the specified ID.</returns>
        public DataTable FetchPunch(int punchId)
        {
            DataTable dtPunch = new DataTable();

            string query = string.Format(
                            @"SELECT
                                h01f01 AS H01101,
                                h01f02 AS H01102
                            FROM pch01
                                WHERE h01f01 = {0};", punchId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dtPunch);

            return dtPunch;
        }

        /// <summary>
        /// Fetches punch data for the specified employee and month.
        /// </summary>
        /// <param name="employeeID">The ID of the employee.</param>
        /// <param name="year">The year of the punch data.</param>
        /// <param name="month">The month of the punch data.</param>
        /// <returns>A DataTable containing the fetched punch data.</returns>
        public DataTable FetchPunchForEmployeeByMonth(int employeeID, int year, int month)
        {
            DataTable dtPunch = new DataTable();

            string query = string.Format(@"
                            SELECT
                                h01f01 AS H01101,
                                h01f02 AS H01102,
                                h01f03 AS H01103,
                                h01f04 AS H01104
                            FROM
                                pch01
                            WHERE
                                h01f02 = {0} AND
                                YEAR(h01f03) = {1} AND
                                MONTH(h01f03) = {2} AND
                                h01f04 != '{3}'",
                            employeeID,
                            year,
                            month,
                            EnmPunchType.D);


            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dtPunch);

            return dtPunch;
        }

        /// <summary>
        /// Fetches ambiguous punch data for the specified date.
        /// </summary>
        /// <param name="date">The date of the ambiguous punch data.</param>
        /// <returns>A DataTable containing the fetched ambiguous punch data.</returns>
        public DataTable FetchAmbiguousPunch(DateTime date)
        {
            DataTable dtPunch = new DataTable();

            string query = string.Format(@"
                            SELECT
                                h01f01 AS H01101,
                                h01f02 AS H01102,
                                h01f03 AS H01103,
                                h01f04 AS H01104
                            FROM
                                pch01
                            WHERE
                                DATE(h01f03) = '{0}' AND
                                h01f04 = '{1}'
                            ORDER BY
                                h01f02, h01f03",
                            date.ToString(Constants.GlobalDateFormat),
                            EnmPunchType.A);


            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dtPunch);

            return dtPunch;
        }
    }
}