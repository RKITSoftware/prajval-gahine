﻿using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System.Data;

namespace FirmAdvanceDemo.DL
{
    public class DBADM00Context
    {
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBUSR01Context"/> class.
        /// </summary>
        public DBADM00Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        /// <summary>
        /// Fetches user data from the 'usr01' table based on the provided userID.
        /// </summary>
        /// <param name="userID">The ID of the user to fetch.</param>
        /// <returns>A DataTable containing user data.</returns>
        public DataTable FetchAdmin(int userID)
        {
            DataTable dtAdmin = new DataTable();

            string query = string.Format(
                            @"
                                SELECT
                                    r01f01 AS r01101,
                                    r01f02 AS r01102,
                                    r01f04 AS r01104,
                                    r01f05 AS r01105
                                FROM
                                    usr01
                                WHERE
                                    r01f01 = {0}", userID);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtAdmin);
            }
            finally
            {
                _connection.Close();
            }

            return dtAdmin;
        }

        /// <summary>
        /// Fetches all user data from the 'usr01' table.
        /// </summary>
        /// <returns>A DataTable containing all user data.</returns>
        public DataTable FetchAdmin()
        {
            DataTable dtAdmin = new DataTable();

            string query = string.Format(
                            @"
                                SELECT
                                    r01f01 AS r01101,
                                    r01f02 AS r01102,
                                    r01f04 AS r01104,
                                    r01f05 AS r01105
                                FROM
                                    usr01");

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtAdmin);
            }
            finally
            {
                _connection.Close();
            }

            return dtAdmin;
        }
    }
}