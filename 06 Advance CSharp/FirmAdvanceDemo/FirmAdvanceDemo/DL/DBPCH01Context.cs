using FirmAdvanceDemo.Connection;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FirmAdvanceDemo.Utility;

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
        /// Retrieves unprocessed punches for a specific date.
        /// </summary>
        /// <param name="date">The date for which to retrieve punches.</param>
        /// <returns>A list of unprocessed punches for the specified date.</returns>
        public List<PCH01> GetUnprocessedPunchesForDate(DateTime date)
        {
            List<PCH01> lstPunch;

            string query = string.Format(@"
                                    SELECT
                                        h01f01,
                                        h01f02,
                                        h01f03,
                                        h01f04
                                    FROM
                                        pch01
                                    WHERE
                                        h01f03 = '{0}'
                                        AND Date(h01f04) = '{1}'
                                    ORDER BY
                                        h01f02, h01f04",
                                        EnmPunchType.U,
                                        date.ToString(Constants.GlobalDateFormat));

            try
            {
                _connection.Open();
                lstPunch = _connection.Query<PCH01>(query)
                    .ToList<PCH01>();
            }
            finally
            {
                _connection.Close();
            }

            return lstPunch;
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

            try
            {
                _connection.Open();
                adapter.Fill(dtPunch);
            }
            finally
            {
                _connection.Close();
            }
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

            try
            {
                _connection.Open();
                adapter.Fill(dtPunch);
            }
            finally
            {
                _connection.Close();
            }
            return dtPunch;
        }
    }
}