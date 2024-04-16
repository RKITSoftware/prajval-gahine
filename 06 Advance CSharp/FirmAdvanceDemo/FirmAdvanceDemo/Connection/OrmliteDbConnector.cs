using ServiceStack.OrmLite;
using System.Configuration;

namespace FirmAdvanceDemo
{
    /// <summary>
    /// Connection class to provide dbFactory instance
    /// </summary>
    public static class OrmliteDbConnector
    {
        /// <summary>
        /// Ormlite Connection Factory - that represent a connection with a particular database
        /// </summary>
        public static OrmLiteConnectionFactory DbFactory { get; set; }

        static OrmliteDbConnector()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connect-to-firmadvance2-db"].ConnectionString;
            DbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }
    }
}