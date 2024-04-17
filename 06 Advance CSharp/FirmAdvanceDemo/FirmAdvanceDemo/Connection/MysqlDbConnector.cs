using MySql.Data.MySqlClient;
using System.Configuration;

namespace FirmAdvanceDemo.Connection
{
    public class MysqlDbConnector
    {
        public static MySqlConnection Connection { get; set; }

        static MysqlDbConnector()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["firmAdvance378"].ConnectionString;
            Connection = new MySqlConnection(connectionString);
        }
    }
}