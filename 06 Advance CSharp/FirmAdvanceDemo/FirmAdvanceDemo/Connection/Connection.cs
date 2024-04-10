using ServiceStack.OrmLite;
using System.Configuration;

namespace FirmAdvanceDemo
{
    /// <summary>
    /// Connection class to provide dbFactory instance
    /// </summary>
    public class Connection
    {
        /// <summary>
        ///  Connection String which is used to connect to intended database
        /// </summary>
        public static string ConnectionString { get; set; }


        /// <summary>
        /// Ormlite Connection Factory - that represent a connection with a particular database
        /// </summary>
        public static OrmLiteConnectionFactory DbFactory { get; set; }

        static Connection()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["connect-to-firmadvance2-db"].ConnectionString;
            DbFactory = new OrmLiteConnectionFactory(ConnectionString, MySqlDialect.Provider);
        }
    }
}