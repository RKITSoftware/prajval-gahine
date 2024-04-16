using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.DB
{
    public class DBDPT01Context
    {
        private readonly MySqlConnection _connection;

        public DBDPT01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        public DataTable SelectDPT01()
        {
            DataTable dtDPT01 = new DataTable();

            string query = @"SELECT
                                d01f01 AS D01101, d01f02 AS D01102
                            FROM dpt01;";

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtDPT01);
            }
            finally
            {
                _connection.Close();
            }
            return dtDPT01;
        }


        public DataTable SelectDPT01(int departmentId)
        {
            DataTable dtDPT01 = new DataTable();

            string query = string.Format(
                            @"SELECT
                                d01f01 AS D01101, d01f02 AS D01102
                            FROM dpt01
                                WHERE d01f01 = {0};", departmentId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtDPT01);
            }
            finally
            {
                _connection.Close();
            }
            return dtDPT01;
        }
    }
}