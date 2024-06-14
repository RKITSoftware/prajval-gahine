using ExpenseSplittingApplication.BL.Domain;
using ExpenseSplittingApplication.Common.Interface;
using NLog.Config;
using ServiceStack;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExpenseSplittingApplication.DL.Interface
{
    /// <summary>
    /// Database context for handling expense-related operations.
    /// </summary>
    public class DBEXP01Context : IDBExpenseContext
    {
        /// <summary>
        /// The database connection.
        /// </summary>
        private IDbConnection _connection;

        /// <summary>
        /// Utility service for common operations.
        /// </summary>
        private IUtility _utility;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBEXP01Context"/> class.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="utility">The utility service.</param>
        public DBEXP01Context(IDbConnection connection, IUtility utility)
        {
            _connection = connection;
            _utility = utility;
        }

        /// <summary>
        /// Gets the list of user IDs that are not in the database.
        /// </summary>
        /// <param name="lstUserID">The list of user IDs to check.</param>
        /// <returns>A list of user IDs that are not found in the database.</returns>
        public List<int> GetUserIdsNotInDatabase(List<int> lstUserID)
        {
            string subquery = string.Join(" UNION ALL ", lstUserID.Select((id, index) =>
            {
                if (index == 0)
                {
                    return $"SELECT {id} as id";
                }
                return $"SELECT {id}";
            }));

            string query = string.Format(@"
                                    SELECT
                                        ids.id
                                    FROM
                                        ({0}) AS ids
                                    WHERE
                                        ids.id NOT IN (SELECT R01F01 FROM USR01)", subquery);

            return _connection.Query<int>(query).ToList();
        }

        /// <summary>
        /// Gets the settlement report for a specific user.
        /// </summary>
        /// <param name="userID">The user ID for which to get the settlement report.</param>
        /// <returns>A <see cref="SettlementReport"/> object containing the user's settlement details.</returns>
        public SettlementReport GetSettlementReport(int userID)
        {
            // check iss bande ne kaha kaha pay kiye and get contributes who have not paid??
            DataTable dataTable = new DataTable();
            string query = string.Empty;

            SettlementReport settlementReport = new SettlementReport();
            settlementReport.UserID = userID;
            query = string.Format(@"
                            SELECT
                                R01F02
                            FROM
                                USR01
                            WHERE
                                R01F01 = {0}", userID);
            settlementReport.Username = _connection.Query<string>(query).FirstOrDefault();

            settlementReport.Receivables = new Dictionary<int, double>();
            settlementReport.Payables = new Dictionary<int, double>();
            settlementReport.FinalDues = new Dictionary<int, double>();

            // receivable query
            query = string.Format(@"
                            SELECT
	                            T01F03,
                                T01F04
                            FROM
	                            EXP01 INNER JOIN CNT01 ON P01F01 = T01F02
                            WHERE
	                            P01F02 = {0} AND T01F05 = 0 AND T01F03 <> {0}", userID);

            dataTable = _utility.ExecuteQuery(query);

            foreach (DataRow row in dataTable.AsEnumerable())
            {
                uint y = 10;
                int x = (int)y;
                int recivableUserID = Convert.ToInt32(row["T01F03"]);
                double amount = Convert.ToDouble(row["T01F04"]);

                if (!settlementReport.Receivables.TryAdd(recivableUserID, amount))
                {
                    settlementReport.Receivables[recivableUserID] += amount;
                }
            }

            // payable query
            query = string.Format(@"
                            SELECT
	                            P01F02,
                                T01F04
                            FROM
	                            EXP01 INNER JOIN CNT01 ON P01F01 = T01F02
                            WHERE
	                            T01F03 = {0} AND T01F05 = 0", userID);

            dataTable = _utility.ExecuteQuery(query);

            foreach (DataRow row in dataTable.AsEnumerable())
            {
                int payableUserID = Convert.ToInt32(row["P01F02"]);
                double amount = Convert.ToDouble(row["T01F04"]);

                if (!settlementReport.Payables.TryAdd(payableUserID, amount))
                {
                    settlementReport.Payables[payableUserID] += amount;
                }
            }

            foreach(KeyValuePair<int, double> payable in settlementReport.Payables)
            {
                double toPayAmount = payable.Value;
                double toRecieveAmount = settlementReport.Receivables.GetValueOrDefault(payable.Key);

                settlementReport.FinalDues.Add(payable.Key, Math.Round(toRecieveAmount - toPayAmount, 2));
            }

            foreach(KeyValuePair<int, double> recievable in settlementReport.Receivables)
            {
                if (!settlementReport.FinalDues.ContainsKey(recievable.Key))
                {
                    settlementReport.FinalDues.Add(recievable.Key, recievable.Value);
                }
            }

            return settlementReport;
        }

        /// <summary>
        /// Gets the due amount between two users.
        /// </summary>
        /// <param name="userID">The user ID who owes the amount.</param>
        /// <param name="payableUserId">The user ID to whom the amount is payable.</param>
        /// <returns>The due amount.</returns>
        public double GetDueAmount(int userID, int payableUserId)
        {
            string dyQuery = @"
                        SELECT
                            COALESCE(SUM(t01f04), 0)
                        FROM
                            cnt01 JOIN exp01 ON t01f02 = p01f01
                        WHERE
                            p01f02 = {0} AND
                            t01f03 = {1} AND
                            t01f05 = 0";

            string query = string.Empty;

            // where user paid for payerUser which is not yet been settled
            query = string.Format(dyQuery, userID, payableUserId);
            double amountToRecieve = _connection.QuerySingleOrDefault<double>(query);

            // where payerUser paid for user which is not yet been settled
            query = string.Format(dyQuery, payableUserId, userID);
            double amountToPay = _connection.QuerySingleOrDefault<double>(query);

            return Math.Round(amountToPay - amountToRecieve, 2);
        }
    }
}

