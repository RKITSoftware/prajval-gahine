using FirmAdvanceDemo.Connection;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using static FirmAdvanceDemo.Utitlity.Constants;

namespace FirmAdvanceDemo.DB
{
    public class DBATD01Context
    {
        private readonly MySqlConnection _connection;

        public DBATD01Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        public DataTable FetchAttendanceByMonthYear(int year, int month)
        {
            DataTable dtAttendance;
            string query = string.Format(
                            @"
                                SELECT
                                    d01f01,
                                    d01f02,
                                    d01f03,
                                    d01f04
                                FROM
                                    atd01
                                WHERE
                                    YEAR(d01f03) = {0} AND
                                    MONTH(d01f03) = {1}",
                            year,
                            month);
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                dtAttendance = new DataTable();
                adapter.Fill(dtAttendance);
            }
            finally
            {
                _connection.Close();
            }
            return dtAttendance;
        }

        public DataTable FetchAttendanceForToday()
        {
            DataTable dtAttendance;
            DateTime now = DateTime.Now;

            string query = string.Format(
                            @"
                                SELECT
                                    d01f01,
                                    d01f02,
                                    d01f03,
                                    d01f04
                                FROM
                                    atd01
                                WHERE
                                    DATE(d01f03) = {0}",
                            now.ToString(GlobalDateFormat));

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                dtAttendance = new DataTable();
                adapter.Fill(dtAttendance);
            }
            finally
            {
                _connection.Close();
            }
            return dtAttendance;
        }

        public DataTable FetchAttendanceByEmployeeIdAndMonthYear(int employeeId, int year, int month)
        {
            DataTable dtAttendance;

            string query = string.Format(
                            @"
                                SELECT
                                    d01f01,
                                    d01f02,
                                    d01f03,
                                    d01f04
                                FROM
                                    atd01
                                WHERE
                                    d01f02 = {0}
                                    YEAR(d01f03) = {1} AND
                                    MONTH(d01f03) = {2}",
                            employeeId,
                            year,
                            month);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                dtAttendance = new DataTable();
                adapter.Fill(dtAttendance);
            }
            finally
            {
                _connection.Close();
            }
            return dtAttendance;
        }

        public DataTable RetrieveAttendanceByEmployeeId(int employeeId)
        {
            DataTable dtAttendance;

            string query = string.Format(
                            @"
                                SELECT
                                    d01f01,
                                    d01f02,
                                    d01f03,
                                    d01f04
                                FROM
                                    atd01
                                WHERE
                                    d01f02 = {0}",
                            employeeId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                dtAttendance = new DataTable();
                adapter.Fill(dtAttendance);
            }
            finally
            {
                _connection.Close();
            }
            return dtAttendance;
        }

        public DataTable FetchAttendance()
        {
            DataTable dtAttendance = new DataTable();

            string query = @"SELECT
                                d01f01 AS D01101,
                                d01f02 AS D01102,
                                d01f03 AS D01103,
                                d01f04 AS D01104
                            FROM atd01";

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtAttendance);
            }
            finally
            {
                _connection.Close();
            }
            return dtAttendance;
        }


        public DataTable FetchAttendance(int attendanceId)
        {
            DataTable dtAttendance = new DataTable();

            string query = string.Format(
                            @"SELECT
                                d01f01 AS D01101, 
                                d01f02 AS D01102
                                d01f03 AS D01103
                                d01f04 AS D01104
                            FROM atd01
                                WHERE d01f01 = {0};", attendanceId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                _connection.Open();
                adapter.Fill(dtAttendance);
            }
            finally
            {
                _connection.Close();
            }
            return dtAttendance;
        }
    }
}