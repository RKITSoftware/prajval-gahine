using FirmAdvanceDemo.Connection;
using FirmAdvanceDemo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using System;
using System.Data;
using static FirmAdvanceDemo.Utitlity.Constants;

namespace FirmAdvanceDemo.DB
{
    public class DBSLY01Context
    {
        private readonly MySqlConnection _connection;
        public DBSLY01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        public MySqlConnection Connection => _connection;

        public DataTable FetchUnpaidWorkHours(DateTime lastCreditDate)
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
                                                                    SUM(d01f04) AS WorkHours,
                                                                FROM
                                                                    atd01
                                                                WHERE
                                                                    DATE(d01f03) > '{0}' AND
                                                                    DATE(d01f03) < '{1}'
                                                                GORUP BY d01f02
                                                            ) AS EmployeeWorkHour ON emp01.p01f01 = EmployeeWorkHour.EmployeeId
                                                INNER JOIN psn01 ON psn01.n01f01",
                                        lastCreditDate.ToString(GlobalDateFormat),
                                        DateTime.Now.ToString(GlobalDateFormat));

            cmd = new MySqlCommand(query, Connection);
            adapter = new MySqlDataAdapter(cmd);
            dtEmployeeWorkHour = new DataTable();

            Connection.Open();
            try
            {
                adapter.Fill(dtEmployeeWorkHour);
            }
            finally
            {
                Connection.Close();
            }
            return dtEmployeeWorkHour;
        }
    }
}