using FirmAdvanceDemo.Connection;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using System;
using System.Data;
using static FirmAdvanceDemo.Utitlity.Constants;

namespace FirmAdvanceDemo.DB
{
    public class DBLVE02Context
    {
        private readonly MySqlConnection _connection;

        public DBLVE02Context()
        {
            _connection = MysqlDbConnector.Connection;
        }

        public bool RequestLeave(LVE02 objLVE02)
        {

            DateTime leaveEndDate = objLVE02.E02F03.AddDays(objLVE02.E02F04 - 1);

            string query = string.Format(@"
                                        SELECT
                                            COUNT(e02f01)
                                        FROM
                                            lve02
                                        WHERE
                                            e02f02 == {0} AND
                                            ( '{1}' >= e02f03 AND '{1}' <= ADDDATE(e02f03, e02f04 - 1) ) OR
                                            ( '{2}' >= e01f03 AND '{2}' <= ADDDATE(e01f03, e01f04 - 1) ) OR
                                            ( '{1}' <= e01f04 AND '{2}' >= ADDDATE(e01f03, e01f04 - 1) )",
                                            objLVE02.E02F02,
                                            objLVE02.E02F03.ToString(GlobalDateFormat),
                                            leaveEndDate.ToString(GlobalDateFormat));

            MySqlCommand cmd = new MySqlCommand(query, _connection);

            _connection.Open();
            try
            {
                
                int conflictCount = (int)cmd.ExecuteScalar();

                if (conflictCount > 0)
                {
                    return false;
                }
            }
            finally
            {
                _connection.Close();
            }

            return true;
        }

        public DataTable FetchLeave()
        {
            DataTable dtLeave;

            string query = string.Format(@"
                                    SELECT
                                        e02f01 AS E02101,
                                        e02f02 AS E02102,
                                        e02f03 AS E02103,
                                        e02f04 AS E02104,
                                        e02f05 AS E02105,
                                        e02f06 AS E02106,
                                        e02f07 AS E02107
                                    FROM
                                        lve02");

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                dtLeave = new DataTable();
                adapter.Fill(dtLeave);
            }
            finally
            {
                _connection.Close();
            }
            return dtLeave;
        }

        public DataTable FetchLeave(int leaveId)
        {
            DataTable dtLeave;

            string query = string.Format(@"
                                    SELECT
                                        e02f01 AS E02101,
                                        e02f02 AS E02102,
                                        e02f03 AS E02103,
                                        e02f04 AS E02104,
                                        e02f05 AS E02105,
                                        e02f06 AS E02106,
                                        e02f07 AS E02107
                                    FROM
                                        lve02
                                    WHERE
                                        e02f01 = {0}",
                                        leaveId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                dtLeave = new DataTable();
                adapter.Fill(dtLeave);
            }
            finally
            {
                _connection.Close();
            }
            return dtLeave;
        }

        public DataTable FetchLeaveByStatus(EnmLeaveStatus leaveStatus)
        {
            DataTable dtLeave;

            string query = string.Format(@"
                                    SELECT
                                        e02f01 AS E02101,
                                        e02f02 AS E02102,
                                        e02f03 AS E02103,
                                        e02f04 AS E02104,
                                        e02f05 AS E02105,
                                        e02f06 AS E02106,
                                        e02f07 AS E02107
                                    FROM
                                        lve02
                                    WHERE
                                        e02f06 = '{0}'",
                                        leaveStatus.ToString());

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                dtLeave = new DataTable();
                adapter.Fill(dtLeave);
            }
            finally
            {
                _connection.Close();
            }
            return dtLeave;
        }

        public DataTable FetchLeaveByEmployee(int employeeId)
        {
            DataTable dtLeave;

            string query = string.Format(@"
                                    SELECT
                                        e02f01 AS E02101,
                                        e02f02 AS E02102,
                                        e02f03 AS E02103,
                                        e02f04 AS E02104,
                                        e02f05 AS E02105,
                                        e02f06 AS E02106,
                                        e02f07 AS E02107
                                    FROM
                                        lve02
                                    WHERE
                                        e02f02 = {0}",
                                        employeeId);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                dtLeave = new DataTable();
                adapter.Fill(dtLeave);
            }
            finally
            {
                _connection.Close();
            }
            return dtLeave;
        }

        public DataTable FetchLeaveByMonthYear(int year, int month)
        {
            DataTable dtLeave;

            string query = string.Format(@"
                                    SELECT
                                        e02f01 AS E02101,
                                        e02f02 AS E02102,
                                        e02f03 AS E02103,
                                        e02f04 AS E02104,
                                        e02f05 AS E02105,
                                        e02f06 AS E02106,
                                        e02f07 AS E02107
                                    FROM
                                        lve02
                                    WHERE
                                        YEAR(e02f03) = {0} AND
                                        MONTH(e02f03) = {1}",
                                        year,
                                        month);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                dtLeave = new DataTable();
                adapter.Fill(dtLeave);
            }
            finally
            {
                _connection.Close();
            }
            return dtLeave;
        }

        public DataTable FetchLeaveByDate(DateTime date)
        {
            DataTable dtLeave;

            string query = string.Format(@"
                                    SELECT
                                        e02f01 AS E02101,
                                        e02f02 AS E02102,
                                        e02f03 AS E02103,
                                        e02f04 AS E02104,
                                        e02f05 AS E02105,
                                        e02f06 AS E02106,
                                        e02f07 AS E02107
                                    FROM
                                        lve02
                                    WHERE
                                        DATE(e01f03) = '{0}'",
                                        date.ToString(GlobalDateFormat));

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                dtLeave = new DataTable();
                adapter.Fill(dtLeave);
            }
            finally
            {
                _connection.Close();
            }
            return dtLeave;
        }

        public DataTable FetchLeaveByEmployeeAndMonthYear(int employeeId, int year, int month)
        {
            DataTable dtLeave;

            string query = string.Format(@"
                                    SELECT
                                        e02f01 AS E02101,
                                        e02f02 AS E02102,
                                        e02f03 AS E02103,
                                        e02f04 AS E02104,
                                        e02f05 AS E02105,
                                        e02f06 AS E02106,
                                        e02f07 AS E02107
                                    FROM
                                        lve02
                                    WHERE
                                        e02f02 = {0} AND
                                        YEAR(e02f03) = {1} AND
                                        MONTH(e02f03) = {2}",
                                        employeeId,
                                        year,
                                        month);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                dtLeave = new DataTable();
                adapter.Fill(dtLeave);
            }
            finally
            {
                _connection.Close();
            }
            return dtLeave;
        }

        public DataTable FetchLeaveByEmployeeAndMonth(int employeeId, int year)
        {
            DataTable dtLeave;

            string query = string.Format(@"
                                    SELECT
                                        e02f01 AS E02101,
                                        e02f02 AS E02102,
                                        e02f03 AS E02103,
                                        e02f04 AS E02104,
                                        e02f05 AS E02105,
                                        e02f06 AS E02106,
                                        e02f07 AS E02107
                                    FROM
                                        lve02
                                    WHERE
                                        e02f02 = {0} AND
                                        YEAR(e02f03) = {1}",
                                        employeeId,
                                        year);

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                _connection.Open();
                dtLeave = new DataTable();
                adapter.Fill(dtLeave);
            }
            finally
            {
                _connection.Close();
            }
            return dtLeave;
        }
    }
}