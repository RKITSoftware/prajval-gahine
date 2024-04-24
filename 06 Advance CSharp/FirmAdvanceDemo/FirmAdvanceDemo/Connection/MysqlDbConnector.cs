using MySql.Data.MySqlClient;
using System.Configuration;

namespace FirmAdvanceDemo.Connection
{
    /// <summary>
    /// Provides a static MySqlConnection instance for connecting to a MySQL database.
    /// </summary>
    public class MysqlDbConnector
    {
        /// <summary>
        /// Gets or sets the MySqlConnection instance.
        /// </summary>
        public static MySqlConnection Connection { get; set; }

        /// <summary>
        /// Static constructor to initialize the MySqlConnection using the connection string from the configuration.
        /// </summary>
        static MysqlDbConnector()
        {
            // Retrieve the connection string from the configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["firmAdvance378"].ConnectionString;

            // Create a new MySqlConnection instance using the retrieved connection string
            Connection = new MySqlConnection(connectionString);
        }
    }
}
