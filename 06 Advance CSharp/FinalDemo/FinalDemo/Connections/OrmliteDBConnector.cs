using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Configuration;

namespace FinalDemo.Connections
{
    /// <summary>
    /// Provides a connector for establishing a database connection using OrmLite.
    /// </summary>
    public class OrmliteDBConnector
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the database connection factory.
        /// </summary>
        public static IDbConnectionFactory DBConnectionFactory { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the OrmliteDBConnector class.
        /// </summary>
        static OrmliteDBConnector()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["finalsampledemotest378"].ConnectionString;
            DBConnectionFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }

        #endregion
    }
}