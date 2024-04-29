using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using static FirmAdvanceDemo.Utility.Constants;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Handles business logic for ATD01 operations.
    /// </summary>
    public class BLATD01Handler
    {
        /// <summary>
        /// The OrmLiteConnectionFactory object.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// The DBATD01Context object.
        /// </summary>
        private readonly DBATD01Context _objDBATD01Context;

        private struct AttendanceProcessingData
        {
            public DateTime dateOfAttendance;
            public List<PCH01> LstInOutPunchesForDate;
            public List<ATD01> LstAttendance;
        }

        private AttendanceProcessingData _attendanceProcessingData;

        /// <summary>
        /// The operation to be performed.
        /// </summary>
        public EnmOperation Operation { get; set; }

        /// <summary>
        /// Initializes a new instance of the BLATD01Handler class.
        /// </summary>
        public BLATD01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _objDBATD01Context = new DBATD01Context();
        }

        /// <summary>
        /// Retrieves attendance records by employee ID.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>The response object.</returns>
        public Response RetrieveAttendanceByEmployeeId(int employeeId)
        {
            Response response = new Response();
            DataTable dtAttendance = _objDBATD01Context.RetrieveAttendanceByEmployeeId(employeeId);
            if (dtAttendance.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No attendance record found for employee: {employeeId}";

                return response;
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtAttendance;

            return response;
        }

        /// <summary>
        /// Retrieves attendance records by month and year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns>The response object.</returns>
        public Response RetrieveAttendanceByMonthYear(int year, int month)
        {
            Response response = new Response();
            DataTable dtAttendance = _objDBATD01Context.FetchAttendanceByMonthYear(year, month);

            if (dtAttendance.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No attendance found for: {year}/{month}";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtAttendance;

            return response;
        }

        /// <summary>
        /// Retrieves attendance records for today.
        /// </summary>
        /// <returns>The response object.</returns>
        public Response RetrieveAttendanceForToday()
        {
            Response response = new Response();

            DataTable dtAttendance = _objDBATD01Context.FetchAttendanceForToday();
            if (dtAttendance.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No attendance records found for today.";

                return response;
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtAttendance;

            return response;
        }

        /// <summary>
        /// Retrieves attendance records by employee ID, year, and month.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns>The response object.</returns>
        public Response RetrieveAttendanceByEmployeeIdAndMonthYear(int employeeId, int year, int month)
        {
            Response response = new Response();

            DataTable dtAttendance = _objDBATD01Context.FetchAttendanceByEmployeeIdAndMonthYear(employeeId, year, month);
            if (dtAttendance.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No attendance found for employee: {employeeId} for year/month: {year}/{month}";

                return response;
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtAttendance;

            return response;
        }

        public void PresaveEoDPunchesForDate(DateTime date)
        {
            _attendanceProcessingData = new AttendanceProcessingData();
            _attendanceProcessingData.dateOfAttendance = date;
            _attendanceProcessingData.LstInOutPunchesForDate = GetInOutPunchesForDate(date);
            _attendanceProcessingData.LstAttendance = ComputeAttendanceFromInOutPunch();
        }

        public Response ValidateEoDAttendance()
        {
            Response response = new Response();
            if (_attendanceProcessingData.LstAttendance.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No attendance generated for date {_attendanceProcessingData.dateOfAttendance.ToString(Constants.GlobalDateFormat)}.";
            }
            return response;
        }

        public Response SaveAttendance()
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction txn = db.OpenTransaction())
                {
                    try
                    {
                        db.UpdateAll<PCH01>(_attendanceProcessingData.LstInOutPunchesForDate);
                        db.InsertAll<ATD01>(_attendanceProcessingData.LstAttendance);
                        txn.Commit();
                    }
                    catch
                    {
                        txn.Rollback();
                        throw;
                    }
                }
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Attendance evaluated for date: {_attendanceProcessingData.dateOfAttendance.ToString(GlobalDateFormat)}";
            return response;
        }

        private List<PCH01> GetInOutPunchesForDate(DateTime date)
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
                                        (h01f03 = '{0}' OR h01f03 = '{1}') AND
                                        Date(h01f04) = '{2}' AND
                                        h01f06 = 0
                                    ORDER BY
                                        h01f02, h01f04",
                                        EnmPunchType.I,
                                        EnmPunchType.O,
                                        date.ToString(Constants.GlobalDateFormat));

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lstPunch = db.Query<PCH01>(query).ToList();
            }
            return lstPunch;
        }

        /// <summary>
        /// Computes attendance records based on the list of punches.
        /// </summary>
        /// <param name="_lstInOutPCH01">The list of punches to process.</param>
        /// <returns>The computed attendance records.</returns>
        private List<ATD01> ComputeAttendanceFromInOutPunch()
        {
            DateTime now = DateTime.Now;

            List<PCH01> lstInOutPunch = _attendanceProcessingData.LstInOutPunchesForDate;
            List<ATD01> lstAttendance = new List<ATD01>();

            double tempWorkHour = 0;
            int size = lstInOutPunch.Count;
            for (int i = 0; i < size; i += 2)
            {
                DateTime punchInTime = lstInOutPunch[i].H01F06;
                DateTime punchOutTime = lstInOutPunch[i + 1].H01F06;
                TimeSpan timeDiff = punchOutTime.Subtract(punchInTime);

                tempWorkHour += timeDiff.TotalHours;

                if ((i + 2 == size) || (lstInOutPunch[i].H01F02 != lstInOutPunch[i + 2].H01F02))
                {
                    lstAttendance.Add(new ATD01
                    {
                        D01F02 = lstInOutPunch[i].H01F02,
                        D01F03 = _attendanceProcessingData.dateOfAttendance,
                        D01F04 = tempWorkHour,
                        D01F06 = now
                    });
                    tempWorkHour = 0;
                }

                // update both in out as considered in attendance
                lstInOutPunch[i].H01F07 = now;
                lstInOutPunch[i + 1].H01F07 = now;

                lstInOutPunch[i].H01F05 = true;
                lstInOutPunch[i + 1].H01F05 = true;
            }
            return lstAttendance;
        }

        /// <summary>
        /// Retrieves attendance records.
        /// </summary>
        /// <returns>The response object.</returns>
        public Response RetrieveAttendance()
        {
            Response response = new Response();
            DataTable dtAttendance = _objDBATD01Context.FetchAttendance();

            if (dtAttendance.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No attendance found.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtAttendance;
            return response;
        }

        /// <summary>
        /// Retrieves attendance records by ID.
        /// </summary>
        /// <param name="attendanceID">The attendance ID.</param>
        /// <returns>The response object.</returns>
        public Response RetrieveAttendance(int attendanceID)
        {
            Response response = new Response();
            DataTable dtAttendance = _objDBATD01Context.FetchAttendance(attendanceID);

            if (dtAttendance.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Attendance {attendanceID} not found.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtAttendance;
            return response;
        }

        /// <summary>
        /// Validates attendance deletion.
        /// </summary>
        /// <param name="attendanceID">The attendance ID.</param>
        /// <returns>The response object.</returns>
        public Response ValidateDelete(int attendanceID)
        {
            Response response = new Response();
            int attendanceCount;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                attendanceCount = (int)db.Count<RLE01>(attendance => attendance.E01F01 == attendanceID);
            }

            if (attendanceCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Attendance {attendanceID} not found.";
            }
            return response;
        }

        /// <summary>
        /// Deletes attendance records.
        /// </summary>
        /// <param name="attendanceID">The attendance ID.</param>
        /// <returns>The response object.</returns>
        public Response Delete(int attendanceID)
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<RLE01>(attendanceID);
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Attendance {attendanceID} deleted.";

            return response;
        }
        /*
/// <summary>
/// Method to fetch all attendance of employee for current month
/// </summary>
/// <param name="EmployeeId">Employee id</param>
/// <returns>ResponseStatusInfo instance containing lst_of_attendance, null if any exception is thrown</returns>
public ResponseStatusInfo FetchAttendanceByEmployeeIdForCurrentMonth(int EmployeeId)
{
try
{
using (IDbConnection db = _dbFactory.OpenDbConnection())
{

DateTime CurrentDate = DateTime.Today;

SqlExpression<ATD01> sqlExp = db.From<ATD01>();
sqlExp.Where(attendance => attendance.D01F02 == EmployeeId)
.And(
"YEAR(D01F03) = {0}",
CurrentDate.Year
)
.And(
"MONTH(D01F03) = {0}",
CurrentDate.Month
);


List<ATD01> lstAttendanceByEmployeeIdForCurrentMonth = db.Select<ATD01>(sqlExp);
return new ResponseStatusInfo()
{
IsRequestSuccessful = true,
Message = $"Attendance list of Employee with employeeId: {EmployeeId} for current month",
Data = lstAttendanceByEmployeeIdForCurrentMonth
};
}
}
catch (Exception ex)
{
return new ResponseStatusInfo()
{
IsRequestSuccessful = false,
Message = ex.Message,
Data = null
};
}
}
*/
    }
}