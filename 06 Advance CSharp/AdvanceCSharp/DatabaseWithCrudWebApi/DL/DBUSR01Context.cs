using DatabaseWithCrudWebApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using static DatabaseWithCrudWebApi.Utility;

namespace DatabaseWithCrudWebApi.Contexts
{
    /// <summary>
    /// Represents a context for interacting with the USR01 table in the database.
    /// </summary>
    public class DBUSR01Context
    {
        #region Private Fields

        /// <summary>
        /// The MySQL database connection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the DBUSR01 class.
        /// </summary>
        public DBUSR01Context()
        {
            _connection = DBConnection.Connection;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserts a new USR01 record into the database.
        /// </summary>
        /// <param name="objUSR01">The USR01 object to insert.</param>
        /// <returns>A ResponseInfo object indicating the result of the operation.</returns>
        public Response InsertUSR01(USR01 objUSR01)
        {
            Response response = new Response();

            string query = string.Format(@"INSERT INTO
	                                            usr01 (r01f02, r01f03, r01f04)
                                            VALUES
                                                ('{0}', '{1}', '{2}');",
                                        objUSR01.R01F02,
                                        objUSR01.R01F03,
                                        objUSR01.R01F04.ToString(GlobalDateTimeFormat));

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            try
            {
                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"User created with id: {objUSR01.R01F01}";

            return response;
        }

        /// <summary>
        /// Updates an existing USR01 record in the database.
        /// </summary>
        /// <param name="objUSR01">The USR01 object with updated values.</param>
        /// <returns>A ResponseInfo object indicating the result of the operation.</returns>
        public Response UpdateUSR01(USR01 objUSR01)
        {
            Response response = new Response();
            string query = string.Format(
                            @"UPDATE
                                usr01
                            SET
                                r01f02 = {0}, r01f03 = {1}, r01f05 = {2}",
                            objUSR01.R01F02,
                            objUSR01.R01F03,
                            objUSR01.R01F05.ToString(GlobalDateTimeFormat));

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            try
            {
                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"User updated with id: {objUSR01.R01F01}";

            return response;
        }

        /// <summary>
        /// Checks if a user with the specified ID exists in the database.
        /// </summary>
        /// <param name="userId">The ID of the user to check.</param>
        /// <returns>True if the user exists, false if not, or null if an error occurred.</returns>
        public bool ExistsUserId(int userId)
        {
            string query = string.Format(
                    "SELECT COUNT(*) AS count FROM USR01 WHERE r01f01 = {0};",
                    userId);
            return ExistsUSR01(query);
        }

        /// <summary>
        /// Checks if a user with the specified username exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the user exists, false if not, or null if an error occurred.</returns>
        public bool ExistsUsername(string username)
        {
            string query = string.Format(
                    @"SELECT
                        COUNT(*) AS count
                    FROM
                        USR01 
                    WHERE
                        r01f02 = '{0}';",
                    username);

            return ExistsUSR01(query);
        }

        /// <summary>
        /// Selects all records from the USR01 table.
        /// </summary>
        /// <returns>A ResponseInfo object containing the result of the operation.</returns>
        public DataTable SelectUSR01()
        {
            DataTable dtUSR01 = new DataTable();
            string query = @"SELECT
                                r01f02 AS r01102, r01f03 AS r01103
                            FROM usr01;";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                dataAdapter.Fill(dtUSR01);
            }
            finally
            {
                _connection.Close();
            }
            return dtUSR01;
        }

        /// <summary>
        /// Selects a single record from the USR01 table based on the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to select.</param>
        /// <returns>A ResponseInfo object containing the result of the operation.</returns>
        public DataTable SelectUSR01(int userId)
        {
            string query = string.Format(
                                    @"SELECT
                                        r01f02 AS r01102, r01f03 AS r01103
                                    FROM
                                        usr01
                                    WHERE
                                        r01f01 = {0}", userId);
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataTable dtUSR01;
            try
            {
                _connection.Open();
                dtUSR01 = new DataTable();
                dataAdapter.Fill(dtUSR01);
            }
            finally
            {
                _connection.Close();
            }
            return dtUSR01;
        }

        /// <summary>
        /// Deletes a user record from the USR01 table based on the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>A ResponseInfo object indicating the result of the operation.</returns>
        public Response DeleteUSR01(int userId)
        {
            Response response = new Response();

            string query = string.Format(
                                        @"DELETE FROM
                                            usr01
                                        WHERE r01f01 = {0};", userId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            try
            {
                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"User deleted with id: {userId}";

            return response;
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Executes a query to check if a record exists in the USR01 table.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <returns>True if the record exists, false if not, or null if an error occurred.</returns>
        private bool ExistsUSR01(string query)
        {
            int count = 0;
            MySqlCommand cmd = new MySqlCommand(query, _connection);

            try
            {
                _connection.Open();
                count = (int) cmd.ExecuteScalar();
            }
            finally
            {
                _connection.Close();
            }

            return count > 0;
        }

        #endregion
    }
}