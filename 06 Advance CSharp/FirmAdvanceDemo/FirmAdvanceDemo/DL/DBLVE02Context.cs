using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System;

namespace FirmAdvanceDemo.DB
{
    public class DBLVE02Context
    {
        private MySqlConnection _connection;

        public DBLVE02Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        public int FetchLeaveConflictCount(int employeeId, DateTime startDateTime, DateTime endDateTime)
        {
            string query = string.Format(@"
                                        SELECT
                                            COUNT(*)
                                        FROM
                                            lve02
                                        WHERE
                                            e02f02 == {0} AND
                                            e02f03
");
        }
    }
}