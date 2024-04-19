using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System.Data;

namespace FirmAdvanceDemo.DB
{
    public class DBRLE01Context
    {
        private readonly MySqlConnection _connection;

        public DBRLE01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

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


        public DataTable FetchRole(int roleId)
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
                                roleId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                dtRole = new DataTable();
                adapter.Fill(dtRole);
            }
            finally
            {
                _connection.Close();
            }
            return dtRole;
        }
    }
}