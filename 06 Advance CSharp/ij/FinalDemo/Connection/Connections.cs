using ServiceStack.OrmLite;
using System.Configuration;

namespace FinalDemo.Connection
{
    /// <summary>
    /// Provides a centralized location for managing database connections using ServiceStack.OrmLite.
    /// </summary>
    public static class Connections
    {

        public static string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        /// <summary>
        /// Gets the OrmLiteConnectionFactory instance for connecting to the database.
        /// </summary>
        public static OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory
               (connection, MySqlDialect.Provider);
    }
}