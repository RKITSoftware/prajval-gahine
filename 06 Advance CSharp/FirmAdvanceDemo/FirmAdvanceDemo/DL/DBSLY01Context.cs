using FirmAdvanceDemo.Connection;
using FirmAdvanceDemo.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using static FirmAdvanceDemo.Utility.Constants;

namespace FirmAdvanceDemo.DB
{
    /// <summary>
    /// Provides methods for interacting with the SLY01 table in the database.
    /// </summary>
    public class DBSLY01Context
    {
        #region Private Fields
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for DBSLY01Context
        /// </summary>
        public DBSLY01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Fetches unpaid work hours for employees based on the last credit date.
        /// </summary>
        /// <param name="lastCreditDate">The last credit date to consider for unpaid work hours calculation.</param>
        /// <returns>A DataTable containing the employeeID, WorkHours, PositionId, and MonthlySalary of employees with unpaid work hours.
        /// </returns>
        public DataTable FetchUnpaidWorkHours(DateTime uptoCreditDate)
        {
            DataTable dtEmployeeWorkHour;
            MySqlCommand cmd;
            MySqlDataAdapter adapter;

            string query = string.Format(@"
                                        SELECT
	                                        employeeID,
	                                        WorkHours,
	                                        n01f01 AS PositionId,
	                                        n01f04 AS MontlhySalary
                                        FROM
	                                        emp01 INNER JOIN (
						                                        SELECT
							                                        d01f02 AS employeeID,
							                                        SUM(
                                                                        CASE
                                                                            WHEN d01f04 > 8 THEN 8
                                                                            ELSE d01f04
                                                                        END
                                                                    ) AS WorkHours
						                                        FROM
							                                        atd01
						                                        WHERE
							                                        DATE(d01f03) < '{0}' AND
                                                                    d01f05 = 0
						                                        GROUP BY d01f02
					                                        ) AS EmployeeWorkHour ON emp01.p01f01 = EmployeeWorkHour.employeeID
		                                        INNER JOIN psn01 ON psn01.n01f01 = emp01.p01f06;",
                                        uptoCreditDate.ToString(GlobalDateFormat));

            cmd = new MySqlCommand(query, _connection);
            adapter = new MySqlDataAdapter(cmd);

            dtEmployeeWorkHour = new DataTable();
            adapter.Fill(dtEmployeeWorkHour);

            return dtEmployeeWorkHour;
        }

        /// <summary>
        /// Fetches salary data for the specified employee within the given date range.
        /// </summary>
        /// <param name="employeeID">The ID of the employee.</param>
        /// <param name="startDate">The start date of the salary data range.</param>
        /// <param name="endDate">The end date of the salary data range.</param>
        /// <returns>A DataTable containing the fetched salary data.</returns>
        public DataTable FetchSalaryForMonth(int year, int month)
        {
            DataTable dtSalary = new DataTable();
            string query = string.Format(@"
                                    SELECT
                                        y01f01 AS 'Salary ID',
                                        y01f02 AS 'Employee ID',
                                        CONCAT(p01f02, ' ', p01f03) AS 'Employee Name',
                                        y01f03 AS 'Amount',
                                        n01f02 AS 'Position',
                                        y01f06 AS 'Credited At'
                                    FROM
                                        sly01 INNER JOIN psn01 ON y01f05 = n01f01 INNER JOIN emp01 ON y01f02 = p01f01
                                    WHERE
                                        YEAR(y01f04) = {0} AND
                                        MONTH(y01f04) = {1}",
                                        year,
                                        month);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dtSalary);
            return dtSalary;
        }

        /// <summary>
        /// Fetches salary data for the specified employee within the given date range.
        /// </summary>
        /// <param name="employeeID">The ID of the employee.</param>
        /// <param name="startDate">The start date of the salary data range.</param>
        /// <param name="endDate">The end date of the salary data range.</param>
        /// <returns>A DataTable containing the fetched salary data.</returns>
        public DataTable FetchSalaryByEmployeeForDateRange(int employeeID, DateTime startDate, DateTime endDate)
        {
            DataTable dtSalary = new DataTable();
            string query = string.Format(@"
                                    SELECT
                                        y01f01 AS 'Salary ID',
                                        y01f03 AS 'Amount',
                                        DATE_FORMAT(y01f04,'""%Y, %M""') as 'Salary Month',
                                        n01f02 AS 'Position',
                                        y01f06 AS 'Credited At'
                                    FROM
                                        sly01 INNER JOIN psn01 ON y01f05 = n01f01 INNER JOIN emp01 ON y01f02 = p01f01
                                    WHERE
                                        y01f02 = {0} AND
                                        y01f04 >= '{1}' AND
                                        y01f04 <= '{2}'",
                                        employeeID,
                                        startDate.ToString(Constants.GlobalDateFormat),
                                        endDate.ToString(Constants.GlobalDateFormat));

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dtSalary);
            return dtSalary;
        }
        #endregion
    }
}