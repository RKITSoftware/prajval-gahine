using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.BL.Master.Service;
using ExpenseSplittingApplication.DL.Context;
using ExpenseSplittingApplication.DL.Interface;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace ExpenseSplittingApplication.Extensions
{
    public static class CustomServicesExtension
    {
        public static void AddApplicationConnections(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDbConnectionFactory>(provider =>
            {
                return new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            });

            services.AddSingleton<IDbConnection>(provider =>
            {
                return new MySqlConnection(connectionString);
            });
        }

        public static void AddBLServices(this IServiceCollection services)
        {
            services.AddScoped<IUSR01Service, BLUSR01Handler>();
            services.AddScoped<IEXP01Service, BLEXP01Handler>();
        }

        public static void AddDBContexts(this IServiceCollection services)
        {
            services.AddSingleton<IDBUserContext, DBUSR01Context>();
            services.AddSingleton<IDBExpenseContext, DBEXP01Context>();
        }
    }
}
