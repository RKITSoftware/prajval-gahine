using ExpenseSplittingApplication.Common.Helper;
using ExpenseSplittingApplication.DL.Interface;
using MySql.Data.MySqlClient;
using System.Data;

namespace ExpenseSplittingApplication.DL.Context
{
    /// <summary>
    /// Database context for handling user-related operations.
    /// </summary>
    public class DBUSR01Context : IDBUserContext
    {
        /// <summary>
        /// The database connection.
        /// </summary>
        private readonly IDbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBUSR01Context"/> class.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="utility">The utility service.</param>
        public DBUSR01Context()
        {
            _connection = new MySqlConnection(Utility.GetConnectionString("378esa"));
        }

        /// <summary>
        /// Executes a SQL query and returns the result as a DataTable.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <returns>A DataTable containing the query results.</returns>
        public DataTable ExecuteQuery(string query)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, (MySqlConnection)_connection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// Retrieves all user records from the database.
        /// </summary>
        /// <returns>A <see cref="DataTable"/> containing all user records.</returns>
        public DataTable GetAll()
        {
            string query = @"
                SELECT
                    R01F01,
                    R01F02
                FROM
                    USR01";

            return ExecuteQuery(query);
        }
    }
}
