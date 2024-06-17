using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace ExpenseSplittingApplication.Common.Helper
{
    public static class EsaOrmliteConnectionFactory
    {
        public static IDbConnectionFactory ConnectionFactory { get; set; }
        static EsaOrmliteConnectionFactory()
        {
            ConnectionFactory = new OrmLiteConnectionFactory(Utility.GetConnectionString("378esa"), MySqlDialect.Provider);
        }
    }
}
