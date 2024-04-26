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
        /// <summary>
        /// The MySqlConnection used for database operations.
        /// </summary>
        private readonly MySqlConnection _connection;

        public DBSLY01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        /// <summary>
        /// Fetches unpaid work hours for employees based on the last credit date.
        /// </summary>
        /// <param name="lastCreditDate">The last credit date to consider for unpaid work hours calculation.</param>
        /// <returns>A DataTable containing the EmployeeId, WorkHours, PositionId, and MonthlySalary of employees with unpaid work hours.
        /// </returns>
        public DataTable FetchUnpaidWorkHours()
        {
            DataTable dtEmployeeWorkHour;
            MySqlCommand cmd;
            MySqlDataAdapter adapter;

            string query = string.Format(@"
                                        SELECT
	                                        EmployeeId,
	                                        WorkHours,
	                                        n01f01 AS PositionId,
	                                        n01f04 AS MontlhySalary
                                        FROM
	                                        emp01 INNER JOIN (
						                                        SELECT
							                                        d01f02 AS EmployeeId,
							                                        SUM(
                                                                        CASE
                                                                            WHEN d01f04 > THEN 8
                                                                            ELSE d01f04
                                                                        END
                                                                    ) AS WorkHours
						                                        FROM
							                                        atd01
						                                        WHERE
							                                        DATE(d01f03) < '{0}' AND
                                                                    d01f07 = 0
						                                        GROUP BY d01f02
					                                        ) AS EmployeeWorkHour ON emp01.p01f01 = EmployeeWorkHour.EmployeeId
		                                        INNER JOIN psn01 ON psn01.n01f01 = emp01.p01f06;",
                                        DateTime.Now.ToString(GlobalDateFormat));

            cmd = new MySqlCommand(query, _connection);
            adapter = new MySqlDataAdapter(cmd);
            dtEmployeeWorkHour = new DataTable();

            _connection.Open();
            try
            {
                adapter.Fill(dtEmployeeWorkHour);
            }
            finally
            {
                _connection.Close();
            }
            return dtEmployeeWorkHour;
        }

        public DataTable FetchSalaryByEmployeeForDateRange(int employeeID, DateTime startDate, DateTime endDate)
        {
            DataTable dtSalary = new DataTable();
            string query = string.Format(@"
                                    SELECT
                                        y01f01 AS 'Salary ID',
                                        y01f02 AS 'Employee ID',
                                        y01f03 AS 'Amount',
                                        y01f04 AS 'Position ID',
                                        y01f05 AS 'Credited At'
                                    FROM
                                        sly01
                                    WHERE
                                        y01f02 = {0} AND
                                        DATE(y01f05) >= '{1}' AND
                                        DATE(y01f05) <= '{2}'",
                                        employeeID,
                                        startDate.ToString(Constants.GlobalDateFormat),
                                        endDate.ToString(Constants.GlobalDateFormat));

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dtSalary);
            return dtSalary;
        }
    }
}