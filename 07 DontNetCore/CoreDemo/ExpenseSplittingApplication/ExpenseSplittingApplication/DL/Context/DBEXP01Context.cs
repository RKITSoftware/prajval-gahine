using ExpenseSplittingApplication.BL.Domain;
using ExpenseSplittingApplication.Common.Interface;
using ServiceStack;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExpenseSplittingApplication.DL.Interface
{
    public class DBEXP01Context : IDBExpenseContext
    {
        private IDbConnection _connection;
        private IUtility _utility;
        public DBEXP01Context(IDbConnection connection, IUtility utility)
        {
            _connection = connection;
            _utility = utility;
        }

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
            settlementReport.Recievables = new Dictionary<int, double>();
            settlementReport.Payables = new Dictionary<int, double>();

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

                if (!settlementReport.Recievables.TryAdd(recivableUserID, amount))
                {
                    settlementReport.Recievables[recivableUserID] += amount;
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

            return settlementReport;
        }

        public double GetDueAmount(int userID, int recievableUserID)
        {
            string dyQuery = @"
                        SELECT
                            SUM(t01f04)
                        FROM
                            cnt01
                        WHERE
                            t01f02 = {0} AND
                            t01f03 = {1} AND
                            t01f05 = 0";

            string query = string.Empty;
            query = string.Format(dyQuery, userID, recievableUserID);

            double amountToRecieve = _connection.QuerySingle<double>(query);
            double amountToPay = _connection.QuerySingle<double>(query);

            return amountToRecieve - amountToPay;
        }
    }
}

