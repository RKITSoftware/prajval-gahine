using ServiceStack.OrmLite;
using System.Configuration;

namespace FirmAdvanceDemo
{
    /// <summary>
    /// Provides a static OrmLiteConnectionFactory instance for connecting to a database using OrmLite.
    /// </summary>
    public static class OrmliteDbConnector
    {
        /// <summary>
        /// Gets or sets the OrmLiteConnectionFactory instance.
        /// </summary>
        public static OrmLiteConnectionFactory DbFactory { get; set; }

        /// <summary>
        /// Static constructor to initialize the OrmLiteConnectionFactory using the connection string from the configuration.
        /// </summary>
        static OrmliteDbConnector()
        {
            // Retrieve the connection string from the configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["firmadvance378"].ConnectionString;

            // Create a new OrmLiteConnectionFactory instance using the retrieved connection string and MySQL dialect provider
            DbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }
    }
}
