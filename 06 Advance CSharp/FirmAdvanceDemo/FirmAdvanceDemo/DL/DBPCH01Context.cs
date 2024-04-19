using FirmAdvanceDemo.Connection;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
                                        h01f01,
                                        h01f02,
                                        h01f03,
                                        h01f04
                                    FROM
                                        pch01
                                    WHERE
                                        h01f03 = {0}
                                        AND Date(h01f04) = {1}
                                    ORDER BY
                                        h01f02, h01f04",
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

        public DataTable FetchPunch()
        {
            DataTable dtPunch = new DataTable();

            string query = @"SELECT
                                h01f01 AS H01101,
                                h01f02 AS H01102,
                                h01f03 AS H01103,
                                h01f04 AS H01104
                            FROM pch01;";

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtPunch);
            }
            finally
            {
                _connection.Close();
            }
            return dtPunch;
        }


        public DataTable FetchPunch(int punchId)
        {
            DataTable dtPunch = new DataTable();

            string query = string.Format(
                            @"SELECT
                                h01f01 AS H01101,
                                h01f02 AS H01102
                            FROM pch01
                                WHERE h01f01 = {0};", punchId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtPunch);
            }
            finally
            {
                _connection.Close();
            }
            return dtPunch;
        }
    }
}