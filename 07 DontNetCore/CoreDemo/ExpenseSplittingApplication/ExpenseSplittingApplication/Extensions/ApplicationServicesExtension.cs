using ExpenseSplittingApplication.BL.Common.Service;
using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.BL.Master.Service;
using ExpenseSplittingApplication.Common.Helper;
using ExpenseSplittingApplication.Common.Interface;
using ExpenseSplittingApplication.DL.Context;
using ExpenseSplittingApplication.DL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace ExpenseSplittingApplication.Extensions
{
    /// <summary>
    /// Extension methods for configuring application services.
    /// </summary>
    public static class ApplicationServicesExtension
    {
        /// <summary>
        /// Adds application services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="connectionString">The database connection string.</param>
        public static void AddApplicationServices(this IServiceCollection services, string connectionString)
        {
            services.AddApplicationConnections(connectionString);
            services.AddBLServices();
            services.AddDBContexts();
            services.AddSingleton<IUtility, Utility>();
            services.AddUserLoggingService();
        }

        /// <summary>
        /// Adds database connection services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="connectionString">The database connection string.</param>
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

        /// <summary>
        /// Adds user logging service to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddUserLoggingService(this IServiceCollection services)
        {
            services.AddTransient<UserLoggerService>((serviceProvider) =>
            {
                IHttpContextAccessor contextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                string userId = contextAccessor.HttpContext.User.FindFirst("userId")?.Value ?? "DefaultUser";
                return new UserLoggerService(userId);
            });
        }

        /// <summary>
        /// Adds business logic services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddBLServices(this IServiceCollection services)
        {
            services.AddScoped<IUSR01Service, BLUSR01Handler>();
            services.AddScoped<IEXP01Service, BLEXP01Handler>();
            services.AddScoped<JwtTokenHandler>();
        }

        /// <summary>
        /// Adds database context services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddDBContexts(this IServiceCollection services)
        {
            services.AddSingleton<IDBUserContext, DBUSR01Context>();
            services.AddSingleton<IDBExpenseContext, DBEXP01Context>();
        }
    }
}
