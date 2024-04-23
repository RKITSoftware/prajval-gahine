using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System.Data;

namespace FirmAdvanceDemo.DB
{
    /// <summary>
    /// Provides methods for interacting with the DPT01 table in the database.
    /// </summary>
    public class DBDPT01Context
    {
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the DBDPT01Context class.
        /// </summary>
        public DBDPT01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        /// <summary>
        /// Fetches all department records.
        /// </summary>
        /// <returns>A DataTable containing all department records.</returns>
        public DataTable FetchDepartment()
        {
            DataTable dtDPT01 = new DataTable();

            string query = @"SELECT
                                t01f01 AS T01101,
                                t01f02 AS T01102
                            FROM dpt01;";

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtDPT01);
            }
            finally
            {
                _connection.Close();
            }
            return dtDPT01;
        }

        /// <summary>
        /// Fetches the department record with the specified department ID.
        /// </summary>
        /// <param name="departmentId">The department ID.</param>
        /// <returns>A DataTable containing the department record.</returns>
        public DataTable FetchDepartment(int departmentId)
        {
            DataTable dtDPT01 = new DataTable();

            string query = string.Format(
                            @"SELECT
                                t01f01 AS T01101,
                                t01f02 AS T01102
                            FROM dpt01
                                WHERE t01f01 = {0};", departmentId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtDPT01);
            }
            finally
            {
                _connection.Close();
            }
            return dtDPT01;
        }
    }
}