using DatabaseWithCRUD.Model;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DatabaseWithCRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            Console.WriteLine(connString);


            // mysql connection
            MySqlConnection _connection = new MySqlConnection(connString);


            // ------------------------------ INSERT ---------------------------------

            USR01 userInsert = new USR01()
            {
                r01f01 = 101,
                r01f02 = "prajval",
                r01f03 = "123"
            };

            using (MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO USR01 (r01f01, r01f02, r01f03) VALUES (@id, @username, @password)", _connection))
            {
                MySqlParameter[] mySqlParameters =
                {
                        new MySqlParameter("@id", userInsert.r01f01),
                        new MySqlParameter("@username", userInsert.r01f02),
                        new MySqlParameter("@password", userInsert.r01f03)
                };
                cmd.Parameters.AddRange(mySqlParameters);

                //cmd.Parameters.AddWithValue("@id", userInsert.r01f01);
                //cmd.Parameters.AddWithValue("@username", userInsert.r01f02);
                //cmd.Parameters.AddWithValue("@password", userInsert.r01f03);

                //cmd.ExecuteNonQuery();
            }


            // -------------------------- SELECT -------------------------

            List<USR01> lstUser = new();

            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM USR01;", _connection))
            {
                _connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // add in user list
                        int id = reader.GetInt32("r01f01");
                        string username = reader.GetString("r01f02");
                        string password = reader.GetString("r01f03");

                        lstUser.Add(new USR01()
                        {
                            r01f01 = id,
                            r01f02 = username,
                            r01f03 = password
                        });
                    }
                }
                _connection.Close();
            }


            // ---------------------------- UPDATE -----------------------------
            using (MySqlCommand cmd = new MySqlCommand(@"
                UPDATE
                    USR01
                SET
                    r01f03 = @password
                WHERE
                    r01f02 = @username", _connection))
            {
                MySqlParameter[] lstMySqlParameter =
                {
                    new MySqlParameter("@password", "987"),
                    new MySqlParameter("@username", "prajval"),
                };
                cmd.Parameters.AddRange(lstMySqlParameter);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }



            // --------------------------- DELETE ------------------------------
            using (MySqlCommand cmd = new MySqlCommand(@"
                DELETE FROM
                    USR01 
                WHERE
                    r01f02 = @username", _connection))
            {
                cmd.Parameters.AddWithValue("@username", "prajval");

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}