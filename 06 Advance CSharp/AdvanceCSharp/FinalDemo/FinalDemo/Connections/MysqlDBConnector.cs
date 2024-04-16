using MySql.Data.MySqlClient;
using System.Configuration;

namespace FinalDemo.Connections
{
    /// <summary>
    /// Provides a connector for establishing a MySQL database connection.
    /// </summary>
    public class MysqlDBConnector
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the MySQL database connection.
        /// </summary>
        public static MySqlConnection DBConnection { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the MysqlDBConnector class.
        /// </summary>
        static MysqlDBConnector()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["finalSampleDemo"].ConnectionString;
            DBConnection = new MySqlConnection(connectionString);
        }

        #endregion
    }
}