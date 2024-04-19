using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System.Data;

namespace FirmAdvanceDemo.DB
{
    public class DBEMP01Context
    {
        private MySqlConnection _connection;

        public DBEMP01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        public DataTable FetchEmployee(int employeeId)
        {
            DataTable dtEmployee = new DataTable();

            string query = string.Format(
                            @"
                                SELECT
                                    
1 AS r01101,
                                    r01f02 AS r01102,
                                    r01f04 AS r01104,
                                    r01f05 AS r01105,
                                    P01F01 AS p01101,
                                    P01F02 AS p01102,
                                    P01F03 AS p01103,
                                    P01F04 AS p01104,
                                    P01F05 AS p01105
                                FROM
                                    usr01 INNER JOIN ump02 ON r01f01 = P01F02
                                          INNER JOIN emp01 ON P01F01 = P01F03
                                WHERE
                                    P01F01 = {0}", employeeId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtEmployee);
            }
            finally
            {
                _connection.Close();
            }

            return dtEmployee;
        }

        public DataTable FetchEmployee()
        {
            DataTable dtEmployee = new DataTable();

            string query = string.Format(
                            @"
                                SELECT
                                    r01f01 AS r01101,
                                    r01f02 AS r01102,
                                    r01f04 AS r01104,
                                    r01f05 AS r01105,
                                    P01F01 AS p01101,
                                    P01F02 AS p01102,
                                    P01F03 AS p01103,
                                    P01F04 AS p01104,
                                    P01F05 AS p01105
                                FROM
                                    usr01 INNER JOIN ump02 ON r01f01 = P01F02
                                          INNER JOIN emp01 ON P01F01 = P01F03");

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtEmployee);
            }
            finally
            {
                _connection.Close();
            }

            return dtEmployee;
        }
    }
}