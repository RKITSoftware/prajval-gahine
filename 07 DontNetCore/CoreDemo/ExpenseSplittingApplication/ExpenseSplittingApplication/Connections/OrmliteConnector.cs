using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace ExpenseSplittingApplication.Connections
{
    public class OrmliteConnector
    {
        public static IDbConnectionFactory ConnectionFactory { get; set; }

        static OrmliteConnector()
        {
            ConnectionFactory = new OrmLiteConnectionFactory();
        }
    }
}
