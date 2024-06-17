
using ExpenseSplittingApplication.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;

namespace ExpenseSplittingApplication.Common.Helper
{
    /// <summary>
    /// Application utility class
    /// </summary>
    public static class Utility
    {
        private static string _connectioString;

        public static void Initialize(string connectionString)
        {
            _connectioString = connectionString;
        }

        /// <summary>
        /// Checks if a user ID exists in the database.
        /// </summary>
        /// <param name="userID">The user ID to check.</param>
        /// <returns>True if the user ID exists, otherwise false.</returns>
        public static bool UserIDExists(int userID)
        {
            bool userIDExists;
            using (IDbConnection db = EsaOrmliteConnectionFactory.ConnectionFactory.OpenDbConnection())
            {
                userIDExists = db.Exists<USR01>(user => user.R01F01 == userID);
            }
            return userIDExists;
        }

        /// <summary>
        /// Checks if a username exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username exists, otherwise false.</returns>
        public static bool UsernameExists(string username)
        {
            bool usernameExists;
            using (IDbConnection db = EsaOrmliteConnectionFactory.ConnectionFactory.OpenDbConnection())
            {
                usernameExists = db.Exists<USR01>(user => user.R01F02 == username);
            }
            return usernameExists;
        }

        public static string GetConnectionString(string dbName = "378esa")
        {
            if(_connectioString == null)
            {
                throw new InvalidOperationException("Connection string has not been initialized");
            }
            return _connectioString + ";database=" + dbName;
        }
    }
}
