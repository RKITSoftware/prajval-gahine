
using FirmAdvanceDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace FirmAdvanceDemo.Utility
{
    /// <summary>
    /// Provides methods for handling general database operations.
    /// </summary>
    public class GeneralHandler
    {
        /// <summary>
        /// The factory for creating IDbConnection instances.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Initializes the GeneralHandler class.
        /// </summary>
        static GeneralHandler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
        }

        /// <summary>
        /// Checks if a user ID exists in the database.
        /// </summary>
        /// <param name="userID">The user ID to check.</param>
        /// <returns>True if the user ID exists; otherwise, false.</returns>
        public static bool CheckUserIDExists(int userID)
        {

            int count = 0;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                count = (int)db.Count<USR01>(user => user.R01F01 == userID);
            }
            return count > 0;
        }

        /// <summary>
        /// Checks if a username exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username exists; otherwise, false.</returns>
        public static bool CheckUsernameExists(string username)
        {
            int count = 0;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                count = (int)db.Count<USR01>(user => user.R01F02 == username);
            }
            return count > 0;
        }

        /// <summary>
        /// Retrieves the username associated with a user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>The username associated with the user ID.</returns>
        public static string RetrieveUsernameByUserID(int userID)
        {

            string username = string.Empty;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                username = db.Scalar<USR01, string>(user => user.R01F01 == userID);
            }

            return username;
        }

        /// <summary>
        /// Retrieves the user ID associated with a username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The user ID associated with the username.</returns>
        public static int RetrieveUserIdByUsername(string username)
        {
            int userId = 0;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                //userId = db.Scalar<USR01, int>(user => user.R01F01,user => user.R01F02 == username);
                var query = db.From<USR01>()
                    .Where(user => user.R01F02 == username)
                    .Select(user => user.R01F01);
                var result = db.Column<int>(query);
                userId = result[0];
            }
            return userId;
        }

        /// <summary>
        /// Retrieves the hashed password associated with a username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The hashed password associated with the username.</returns>
        public static string RetrievePassword(string username)
        {
            string hashedPassword = string.Empty;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                hashedPassword = db.Scalar<USR01, string>(user => user.R01F03, user => user.R01F02 == username);
            }
            return hashedPassword;
        }

        /// <summary>
        /// Retrieves the employee ID associated with a user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>The employee ID associated with the user ID.</returns>
        public static int RetrieveEmployeeIDByUserID(int userID)
        {
            int employeeID = 0;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                employeeID = db.Scalar<UMP02, int>(userEmployee => userEmployee.P02F03, userEmployee => userEmployee.P02F02 == userID);
            }

            return employeeID;
        }
    }
}