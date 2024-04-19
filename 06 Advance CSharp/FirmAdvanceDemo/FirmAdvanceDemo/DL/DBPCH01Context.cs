using FirmAdvanceDemo.Connection;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using static FirmAdvanceDemo.Utitlity.Constants;

namespace FirmAdvanceDemo.DB
{
    public class DBPCH01Context
    {
        private readonly MySqlConnection _connection;

        public DBPCH01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        public List<PCH01> GetUnprocessedPunchesForDate(DateTime date)
        {
            List<PCH01> lstPunch;

            string query = string.Format(@"
                                    SELECT
                                        H01F01,
                                        H01F02,
                                        H01F03,
                                        H01F04
                                    FROM
                                        pch01
                                    WHERE
                                        H01F03 = {0}
                                        AND Date(H01F04) = {1}
                                    ORDER BY
                                        H01F02, H01F04",
                                        EnmPunchType.U,
                                        date.ToString(GlobalDateFormat));

            try
            {
                _connection.Open();
                lstPunch = _connection.Query<PCH01>(query)
                    .ToList<PCH01>();
            }
            finally
            {
                _connection.Close();
            }

            return lstPunch;
        }
    }
}