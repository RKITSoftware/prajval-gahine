using FirmAdvanceDemo.Connection;
using FirmAdvanceDemo.Enums;
using MySql.Data.MySqlClient;
using System.Data;

namespace FirmAdvanceDemo.DL
{
    public class DBADM00Context
    {
        #region Private Fields
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DBADM00Context"/> class.
        /// </summary>
        public DBADM00Context()
        {
            _connection = MysqlDbConnector.Connection;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Fetches admin data from the 'usr01' table based on the provided userID.
        /// </summary>
        /// <param name="userID">The ID of the user to fetch.</param>
        /// <returns>A DataTable containing admin data.</returns>
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


            adapter.Fill(dtAdmin);

            return dtAdmin;
        }

        /// <summary>
        /// Fetches all admin data from the 'usr01' table.
        /// </summary>
        /// <returns>A DataTable containing all admin data.</returns>
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
	                                usr01 INNER JOIN ule02 ON r01f01 = e02f02
                                    INNER JOIN rle01 ON e02f03 = e01f01
                                WHERE
	                                e01f02 = '{0}'",
                            EnmRole.A);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dtAdmin);

            return dtAdmin;
        }
        #endregion
    }
}