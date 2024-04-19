
using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FirmAdvanceDemo.Utitlity
{
    public class GeneralContext
    {
        private static readonly MySqlConnection _connection;

        static GeneralContext()
        {
            _connection = MysqlDbConnector.Connection;
        }

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
                                                        ) AS tmp01 ON r01f01 = tmp01.e02f03",
                                                        userID);

            dtUserRole = new DataTable();
            cmd = new MySqlCommand(query, _connection);
            adapter = new MySqlDataAdapter(cmd);

            _connection.Open();
            try
            {
                adapter.Fill(dtUserRole);
            }
            finally
            {
                _connection.Close();
            }
            lstUserRole = dtUserRole.AsEnumerable().Select(row => (string)row["e01f02"]).ToArray();
            return lstUserRole;
        }
    }
}