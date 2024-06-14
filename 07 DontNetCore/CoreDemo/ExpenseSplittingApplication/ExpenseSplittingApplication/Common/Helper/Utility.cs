using ExpenseSplittingApplication.Common.Interface;
using ExpenseSplittingApplication.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace ExpenseSplittingApplication.Common.Helper
{
    /// <summary>
    /// Application utility class
    /// </summary>
    public class Utility : IUtility
    {
        /// <summary>
        /// Factory for creating database connections.
        /// </summary>
        private IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Represents a database connection.
        /// </summary>
        private IDbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Utility"/> class.
        /// </summary>
        /// <param name="dbFactory">Factory for creating database connections.</param>
        /// <param name="connection">Represents a database connection.</param>
        public Utility(IDbConnectionFactory dbFactory, IDbConnection connection)
        {
            _dbFactory = dbFactory;
            _connection = connection;
        }

        /// <summary>
        /// Checks if a user ID exists in the database.
        /// </summary>
        /// <param name="userID">The user ID to check.</param>
        /// <returns>True if the user ID exists, otherwise false.</returns>
        public bool UserIDExists(int userID)
        {
            bool userIDExists;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
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
        public bool UsernameExists(string username)
        {
            bool usernameExists;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                usernameExists = db.Exists<USR01>(user => user.R01F02 == username);
            }
            return usernameExists;
        }

        /// <summary>
        /// Executes a SQL query and returns the result as a DataTable.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <returns>A DataTable containing the query results.</returns>
        public DataTable ExecuteQuery(string query)
        {
            IDbConnection connection = new MySqlConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, (MySqlConnection)_connection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }
    }
}
