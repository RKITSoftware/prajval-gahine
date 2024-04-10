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
        public static string ConnString;


        /// <summary>
        /// Ormlite Connection Factory - that represent a connection with a particular database
        /// </summary>
        public static OrmLiteConnectionFactory dbFactory;

        static Connection()
        {
            ConnString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);
        }
    }
}