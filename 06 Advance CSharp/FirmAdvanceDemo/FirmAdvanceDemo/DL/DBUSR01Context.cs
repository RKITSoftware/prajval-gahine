using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System.Data;

namespace FirmAdvanceDemo.DB
{
    public class DBUSR01Context
    {
        private readonly MySqlConnection _connection;

        public DBUSR01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        public MySqlConnection Connection => _connection;

        public DataTable FetchUser(int userID)
        {
            DataTable dtUser = new DataTable();

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
                                    P01F01 = {0}", userID);

            MySqlCommand cmd = new MySqlCommand(query, Connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                Connection.Open();
                adapter.Fill(dtUser);
            }
            finally
            {
                Connection.Close();
            }

            return dtUser;
        }

        public DataTable FetchUser()
        {
            DataTable dtUser = new DataTable();

            string query = string.Format(
                            @"
                                SELECT
                                    r01f01 AS r01101,
                                    r01f02 AS r01102,
                                    r01f04 AS r01104,
                                    r01f05 AS r01105
                                FROM
                                    usr01");

            MySqlCommand cmd = new MySqlCommand(query, Connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                Connection.Open();
                adapter.Fill(dtUser);
            }
            finally
            {
                Connection.Close();
            }

            return dtUser;
        }
    }
}