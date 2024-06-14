using System.Data;

namespace ExpenseSplittingApplication.Common.Interface
{
    /// <summary>
    /// Utitlity interface
    /// </summary>
    public interface IUtility
    {
        /// <summary>
        /// Checks if a user ID exists in the database.
        /// </summary>
        /// <param name="userID">The user ID to check.</param>
        /// <returns>True if the user ID exists, otherwise false.</returns>
        public bool UserIDExists(int userID);

        /// <summary>
        /// Checks if a username exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username exists, otherwise false.</returns>
        public bool UsernameExists(string username);

        /// <summary>
        /// Executes a SQL query and returns the result as a DataTable.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <returns>A DataTable containing the query results.</returns>
        public DataTable ExecuteQuery(string query);
    }
}
