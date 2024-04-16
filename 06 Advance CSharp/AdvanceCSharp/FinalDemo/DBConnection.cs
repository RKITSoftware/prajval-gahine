using MySql.Data.MySqlClient;
using System.Configuration;

namespace DatabaseWithCrudWebApi
{
    /// <summary>
    /// Provides a static MySqlConnection object for database connection.
    /// </summary>
    public static class DBConnection
    {
        #region Public Properties

        /// <summary>
        /// The MySqlConnection object for database connection.
        /// </summary>
        public static MySqlConnection Connection { get; set; }

        #endregion

        #region Constructor

        static DBConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            Connection = new MySqlConnection(connectionString);
        }

        #endregion
    }
}
