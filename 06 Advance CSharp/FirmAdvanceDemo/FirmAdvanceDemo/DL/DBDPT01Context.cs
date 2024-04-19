using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System.Data;

namespace FirmAdvanceDemo.DB
{
    public class DBDPT01Context
    {
        private readonly MySqlConnection _connection;

        public DBDPT01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

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