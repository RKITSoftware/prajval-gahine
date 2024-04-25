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

        private class AttendanceData
        {
            public DateTime dateOfAttendance;
            public List<PCH01> LstAllPunch;
            public List<PCH01> LstInOutPunch;
            public List<ATD01> LstAttendance;
        }

        private AttendanceData _attendanceData;

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

        public void PresaveEoDPunchesForAttendance(DateTime date)
        {
            _attendanceData = new AttendanceData
            {
                dateOfAttendance = date,
                LstAllPunch = GetUnprocessedPunchesForDate(date)
            };
            UpdateAllPunchType();
            _attendanceData.LstInOutPunch = FilterInOutPunches();
            _attendanceData.LstAttendance = ComputeAttendanceFromInOutPunch();
        }

        public Response ValidateEoDPunchesForAttendance()
        {
            Response response = new Response();
            if (_attendanceData.LstAttendance.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No attendance generated for date {_attendanceData.dateOfAttendance.ToString(Constants.GlobalDateFormat)}";
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
                        db.UpdateAll<PCH01>(_attendanceData.LstAllPunch);
                        db.InsertAll<ATD01>(_attendanceData.LstAttendance);
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
            response.Message = $"Attendance evaluated for date: {_attendanceData.dateOfAttendance.ToString(GlobalDateFormat)}";
            return response;
        }

        private List<PCH01> GetUnprocessedPunchesForDate(DateTime date)
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
                                        h01f03 = '{0}' AND
                                        Date(h01f04) = '{1}' AND
                                        h01f06 = 0
                                    ORDER BY
                                        h01f02, h01f04",
                                        EnmPunchType.U,
                                        date.ToString(Constants.GlobalDateFormat));

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lstPunch = db.Query<PCH01>(query).ToList();
            }
            return lstPunch;
        }

        /// <summary>
        /// Updates the punch type based on certain criteria.
        /// </summary>
        /// <param name="lstPCH01">The list of punches to process.</param>
        private void UpdateAllPunchType()
        {
            MarkMistakenlyPunch();
            MarkAmbiguousPunch();
            MarkInOutPunch();
        }

        private List<PCH01> FilterInOutPunches()
        {
            List<PCH01> lstInOutPunch;
            lstInOutPunch = _attendanceData.LstAllPunch.Where(punch => punch.H01F03 == EnmPunchType.I || punch.H01F03 == EnmPunchType.O)
                .ToList();
            return lstInOutPunch;
        }

        /// <summary>
        /// Marks punches that are mistakenly recorded.
        /// </summary>
        /// <param name="_lstPCH01">The list of punches to process.</param>
        private void MarkMistakenlyPunch()
        {
            List<PCH01> lstAllPunch = _attendanceData.LstAllPunch;
            TimeSpan timeBuffer = new TimeSpan(0, 0, 10);    // 10 second buffer
            int size = lstAllPunch.Count;
            for (int i = 0; i < size - 1; i++)
            {
                PCH01 currPunch = lstAllPunch[i];
                PCH01 nextPunch = lstAllPunch[i + 1];
                if (currPunch.H01F02 == nextPunch.H01F02 && nextPunch.H01F04.Subtract(currPunch.H01F04) <= timeBuffer)
                {
                    currPunch.H01F03 = EnmPunchType.M;
                }
            }
        }

        /// <summary>
        /// Marks punches that are ambiguous or unclear.
        /// </summary>
        /// <param name="_lstPCH01">The list of punches to process.</param>
        private void MarkAmbiguousPunch()
        {
            List<PCH01> lstAllPunch = _attendanceData.LstAllPunch;
            int startIndex = 0;
            int endIndex = 0;
            int size = lstAllPunch.Count;
            int uPunchForEmployeeCount = 0;

            for (int i = 0; i < size; i++)
            {
                PCH01 currPunch = lstAllPunch[i];
                PCH01 nextPunch = (i + 1 >= size) ? null : lstAllPunch?[i + 1];

                if (currPunch.H01F03 == EnmPunchType.U)
                {
                    uPunchForEmployeeCount++;
                }

                if (nextPunch == null || currPunch.H01F02 != nextPunch.H01F02)
                {
                    if (uPunchForEmployeeCount % 2 != 0)
                    {
                        for (int j = startIndex; j <= endIndex; j++)
                        {
                            if (lstAllPunch[j].H01F03 == EnmPunchType.U)
                            {
                                lstAllPunch[j].H01F03 = EnmPunchType.A;
                            }
                        }
                    }
                    uPunchForEmployeeCount = 0;
                    startIndex = i + 1;
                    endIndex = startIndex;
                }
                else
                {
                    endIndex++;
                }
            }
        }

        /// <summary>
        /// Marks punches as In/Out based on certain criteria.
        /// </summary>
        /// <param name="_lstPCH01">The list of punches to process.</param>
        private void MarkInOutPunch()
        {
            List<PCH01> lstAllPunch = _attendanceData.LstAllPunch;

            bool isEmployeePunchIn = false;
            foreach (PCH01 punch in lstAllPunch)
            {
                if (punch.H01F03 == EnmPunchType.U)
                {
                    if (isEmployeePunchIn)
                    {
                        punch.H01F03 = EnmPunchType.O;
                    }
                    else
                    {
                        punch.H01F03 = EnmPunchType.I;
                    }
                    punch.H01F06 = true;
                    isEmployeePunchIn = !isEmployeePunchIn;
                }
            }
        }

        /// <summary>
        /// Computes attendance records based on the list of punches.
        /// </summary>
        /// <param name="_lstInOutPCH01">The list of punches to process.</param>
        /// <returns>The computed attendance records.</returns>
        private List<ATD01> ComputeAttendanceFromInOutPunch()
        {
            List<PCH01> lstInOutPunch = _attendanceData.LstInOutPunch;
            List<ATD01> lstAttendance = new List<ATD01>();

            DateTime now = DateTime.Now;    // current datetime

            double tempWorkHour = 0;
            int size = lstInOutPunch.Count;
            for (int i = 0; i < size; i += 2)
            {
                DateTime punchInTime = lstInOutPunch[i].H01F04;
                DateTime punchOutTime = lstInOutPunch[i + 1].H01F04;
                TimeSpan timeDiff = punchOutTime.Subtract(punchInTime);

                tempWorkHour += timeDiff.TotalHours;

                if ((i + 2 == size) || (lstInOutPunch[i].H01F02 != lstInOutPunch[i + 2].H01F02))
                {
                    lstAttendance.Add(new ATD01
                    {
                        D01F02 = lstInOutPunch[i].H01F02,
                        D01F03 = _attendanceData.dateOfAttendance,
                        D01F04 = tempWorkHour,
                        D01F05 = now
                    });
                    tempWorkHour = 0;
                }
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