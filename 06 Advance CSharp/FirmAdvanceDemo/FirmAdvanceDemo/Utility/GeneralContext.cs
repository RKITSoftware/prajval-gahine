
using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;

namespace FirmAdvanceDemo.Utility
{
    /// <summary>
    /// Provides methods for handling general database context operations.
    /// </summary>
    public class GeneralContext
    {
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private static readonly MySqlConnection _connection;

        /// <summary>
        /// Initializes the GeneralContext class.
        /// </summary>
        static GeneralContext()
        {
            _connection = MysqlDbConnector.Connection;
        }

        /// <summary>
        /// Fetches the roles associated with a user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>An array of role names associated with the user ID.</returns>
        public static string[] FetchRolesByUserID(int userID)
        {
            string[] lstUserRole;
            DataTable dtUserRole;
            MySqlCommand cmd;
            MySqlDataAdapter adapter;

            string query = string.Format(@"
                                    SELECT
                                        e01f02
                                    FROM
                                        rle01 INNER JOIN (
                                                            SELECT
                                                                e02f03
                                                            FROM
                                                                ule02
                                                            WHERE
                                                                e02f02 = {0}
                                                        ) AS tmp01 ON e01f01 = tmp01.e02f03",
                                                        userID);

            dtUserRole = new DataTable();
            cmd = new MySqlCommand(query, _connection);
            adapter = new MySqlDataAdapter(cmd);


            adapter.Fill(dtUserRole);

            lstUserRole = dtUserRole.AsEnumerable().Select(row => (string)row["e01f02"]).ToArray();
            return lstUserRole;
        }
    }
}