using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System.Data;

namespace FirmAdvanceDemo.DB
{
    /// <summary>
    /// Provides methods for interacting with the EMP01 table in the database.
    /// </summary>
    public class DBEMP01Context
    {
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBEMP01Context"/> class.
        /// </summary>
        public DBEMP01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        /// <summary>
        /// Gets the MySqlConnection used for database operations.
        /// </summary>
        public MySqlConnection Connection => _connection;

        /// <summary>
        /// Fetches the employee record with the specified employee ID.
        /// </summary>
        /// <param name="employeeID">The employee ID.</param>
        /// <returns>A DataTable containing the employee record.</returns>
        public DataTable FetchEmployee(int employeeID)
        {
            DataTable dtEmployee = new DataTable();

            string query = string.Format(
                            @"
                                SELECT
                                    r01f01 AS r01101,
                                    r01f02 AS r01102,
                                    r01f04 AS r01104,
                                    r01f05 AS r01105,
                                    P01F01 AS p01101,
                                    P01F02 AS p01102,
                                    P01F03 AS p01103,
                                    P01F04 AS p01104,
                                    P01F05 AS p01105
                                FROM
                                    usr01 INNER JOIN ump02 ON r01f01 = p02f02
                                          INNER JOIN emp01 ON p01f01 = p02f03
                                WHERE
                                    P01F01 = {0}", employeeID);

            MySqlCommand cmd = new MySqlCommand(query, Connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);


            adapter.Fill(dtEmployee);

            return dtEmployee;
        }

        /// <summary>
        /// Fetches all employee records.
        /// </summary>
        /// <returns>A DataTable containing all employee records.</returns>
        public DataTable FetchEmployee()
        {
            DataTable dtEmployee = new DataTable();

            string query = string.Format(
                            @"
                                SELECT
                                    r01f01 AS r01101,
                                    r01f02 AS r01102,
                                    r01f04 AS r01104,
                                    r01f05 AS r01105,
                                    P01F01 AS p01101,
                                    P01F02 AS p01102,
                                    P01F03 AS p01103,
                                    P01F04 AS p01104,
                                    P01F05 AS p01105
                                FROM
                                    usr01 INNER JOIN ump02 ON r01f01 = p02f02
                                          INNER JOIN emp01 ON p01f01 = p02f03");

            MySqlCommand cmd = new MySqlCommand(query, Connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(dtEmployee);

            return dtEmployee;
        }
    }
}