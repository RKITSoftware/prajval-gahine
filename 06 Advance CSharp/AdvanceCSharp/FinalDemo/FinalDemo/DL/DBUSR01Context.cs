using FinalDemo.Connections;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FinalDemo.DL
{
    /// <summary>
    /// Provides data access methods for the USR01 entity.
    /// </summary>
    public class DBUSR01Context
    {
        #region Private Fields

        /// <summary>
        /// Represents the database connection used by the context.
        /// </summary>
        private readonly MySqlConnection _connection;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DBUSR01Context class.
        /// </summary>
        public DBUSR01Context()
        {
            _connection = MysqlDBConnector.DBConnection;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves all user records from the database.
        /// </summary>
        /// <returns>A DataTable containing the user records.</returns>
        public DataTable SelectUSR01()
        {
            DataTable dtUSR01 = new DataTable();

            string query = string.Format(@"SELECT
                                                r01f01 AS r01101, r01f02 AS r01102
                                            FROM
                                                usr01");
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                adapter.Fill(dtUSR01);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
            return dtUSR01;
        }

        /// <summary>
        /// Retrieves a specific user record from the database based on the provided id.
        /// </summary>
        /// <param name="id">The id of the user to retrieve.</param>
        /// <returns>A DataTable containing the user record.</returns>
        public DataTable SelectUSR01(int id)
        {
            DataTable dtUSR01 = new DataTable();

            string query = string.Format(@"SELECT
                                                r01f01 AS r01101, r01f02 AS r01102
                                            FROM
                                                usr01
                                            WHERE
                                                r01f01 = {0}", id);
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                adapter.Fill(dtUSR01);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }

            return dtUSR01;
        }

        #endregion
    }
}