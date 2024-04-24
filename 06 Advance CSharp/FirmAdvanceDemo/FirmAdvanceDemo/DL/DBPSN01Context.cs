using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System.Data;

namespace FirmAdvanceDemo.DB
{
    /// <summary>
    /// Provides methods for interacting with the PSN01 table in the database.
    /// </summary>
    public class DBPSN01Context
    {
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBPSN01Context"/> class.
        /// </summary>
        public DBPSN01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        /// <summary>
        /// Fetches all position records.
        /// </summary>
        /// <returns>A DataTable containing all position records.</returns>
        public DataTable FetchPosition()
        {
            DataTable dtPSN01 = new DataTable();

            string query = string.Format(@"
                            SELECT
                                N01F01 AS N01101,
                                N01F02 AS N01102,
                                N01F03 AS N01103,
                                N01F04 AS N01104,
                                N01F05 AS N01105,
                                N01F06 AS N01106
                            FROM psn01");

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtPSN01);
            }
            finally
            {
                _connection.Close();
            }
            return dtPSN01;
        }

        /// <summary>
        /// Fetches a specific position record by its ID.
        /// </summary>
        /// <param name="positionID">The ID of the position record to fetch.</param>
        /// <returns>A DataTable containing the position record with the specified ID.</returns>
        public DataTable FetchPosition(int positionID)
        {
            DataTable dtPosition;

            string query = string.Format(@"
                            SELECT
                                n01f01 AS N01101,
                                n01f02 AS N01102,
                                n01f03 AS N01103,
                                n01f04 AS N01104,
                                n01f05 AS N01105,
                                n01f06 AS N01106
                            FROM
                                psn01
                            WHERE
                                n01f01 = {0}",
                                positionID);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                dtPosition = new DataTable();
                adapter.Fill(dtPosition);
            }
            finally
            {
                _connection.Close();
            }
            return dtPosition;
        }
    }
}