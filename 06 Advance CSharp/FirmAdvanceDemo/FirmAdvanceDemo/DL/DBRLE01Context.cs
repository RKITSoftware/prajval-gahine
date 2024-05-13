using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System.Data;

namespace FirmAdvanceDemo.DB
{
    /// <summary>
    /// Provides methods for interacting with the RLE01 table in the database.
    /// </summary>
    public class DBRLE01Context
    {
        #region Private Fields
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DBRLE01Context"/> class.
        /// </summary>

        public DBRLE01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Fetches all role records.
        /// </summary>
        /// <returns>A DataTable containing all role records.</returns>
        public DataTable FetchRole()
        {
            DataTable dtPSN01 = new DataTable();

            string query = string.Format(@"
                            SELECT
                                e01f01 AS E01101,
                                e01f02 AS E01102
                            FROM rle01");

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);


            adapter.Fill(dtPSN01);

            return dtPSN01;
        }

        /// <summary>
        /// Fetches a specific role record by its ID.
        /// </summary>
        /// <param name="roleID">The ID of the role record to fetch.</param>
        /// <returns>A DataTable containing the role record with the specified ID.</returns>
        public DataTable FetchRole(int roleID)
        {
            DataTable dtRole;

            string query = string.Format(@"
                            SELECT
                                e01f01 AS E01101,
                                e01f02 AS E01102
                            FROM
                                rle01
                            WHERE
                                e01f01 = {0}",
                                roleID);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);


            dtRole = new DataTable();
            adapter.Fill(dtRole);

            return dtRole;
        }
        #endregion
    }
}