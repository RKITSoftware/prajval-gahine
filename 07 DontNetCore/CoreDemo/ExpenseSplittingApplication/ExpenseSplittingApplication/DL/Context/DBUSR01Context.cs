using ExpenseSplittingApplication.Common.Interface;
using ExpenseSplittingApplication.DL.Interface;
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
        /// Utility service for common operations.
        /// </summary>
        private readonly IUtility _utility;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBUSR01Context"/> class.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="utility">The utility service.</param>
        public DBUSR01Context(IDbConnection connection, IUtility utility)
        {
            _connection = connection;
            _utility = utility;
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

            return _utility.ExecuteQuery(query);
        }
    }
}
