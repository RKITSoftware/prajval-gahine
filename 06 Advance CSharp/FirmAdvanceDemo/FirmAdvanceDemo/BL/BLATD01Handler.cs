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

        /// <summary>
        /// Represents the data needed for processing attendance.
        /// </summary>
        public struct AttendanceProcessingData
        {
            /// <summary>
            /// The date for which attendance is being processed.
            /// </summary>
            public DateTime dateOfAttendance;

            /// <summary>
            /// The list of punch entries for the specified date.
            /// </summary>
            public List<PCH01> LstInOutPunchesForDate;

            /// <summary>
            /// The list of existing attendance entries for the specified date.
            /// </summary>
            public List<ATD01> LstAttendance;
        }

        /// <summary>
        /// The data for processing attendance.
        /// </summary>
        public AttendanceProcessingData _attendanceProcessingData;

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
        /// <param name="employeeID">The employee ID.</param>
        /// <returns>The response object.</returns>
        public Response RetrieveAttendanceByemployeeID(int employeeID)
        {
            Response response = new Response();
            DataTable dtAttendance = _objDBATD01Context.RetrieveAttendanceByemployeeID(employeeID);
            if (dtAttendance.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No attendance record found for employee: {employeeID}";

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
        /// <param name="employeeID">The employee ID.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns>The response object.</returns>
        public Response RetrieveAttendanceByemployeeIDAndMonthYear(int employeeID, int year, int month)
        {
            Response response = new Response();

            DataTable dtAttendance = _objDBATD01Context.FetchAttendanceByemployeeIDAndMonthYear(employeeID, year, month);
            if (dtAttendance.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No attendance found for employee: {employeeID} for year/month: {year}/{month}";

                return response;
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtAttendance;

            return response;
        }

        /// <summary>
        /// Prepares attendance processing data for the end of day (EoD) punches for the specified date.
        /// </summary>
        /// <param name="date">The date for which EoD punches are being processed.</param>
        public void PresaveEoDPunchesForDate(DateTime date)
        {
            _attendanceProcessingData = new AttendanceProcessingData();
            _attendanceProcessingData.dateOfAttendance = date;
            _attendanceProcessingData.LstInOutPunchesForDate = GetInOutPunchesForDate(date);
            _attendanceProcessingData.LstAttendance = ComputeAttendanceFromInOutPunch();
        }

        /// <summary>
        /// Validates the end of day (EoD) attendance data.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
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

        /// <summary>
        /// Saves the computed attendance data.
        /// </summary>
        /// <returns>A response indicating the success of the save operation.</returns>
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
            response.Message = $"Attendance evaluated for date: {_attendanceProcessingData.dateOfAttendance.ToString(GlobalDateFormat)}.";
            return response;
        }

        /// <summary>
        /// Retrieves the list of in-out punches for the specified date.
        /// </summary>
        /// <param name="date">The date for which to retrieve punches.</param>
        /// <returns>The list of in-out punches for the specified date.</returns>
        public List<PCH01> GetInOutPunchesForDate(DateTime date)
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
                                        (h01f04 = '{0}' OR h01f04 = '{1}') AND
                                        Date(h01f03) = '{2}' AND
                                        h01f05 = 0
                                    ORDER BY
                                        h01f02, h01f03",
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
        /// Computes the attendance data from the in-out punches.
        /// </summary>
        /// <returns>The computed attendance data.</returns>
        public List<ATD01> ComputeAttendanceFromInOutPunch()
        {
            DateTime now = DateTime.Now;

            List<PCH01> lstInOutPunch = _attendanceProcessingData.LstInOutPunchesForDate;
            List<ATD01> lstAttendance = new List<ATD01>();

            double tempWorkHour = 0;
            int size = lstInOutPunch.Count;
            for (int i = 0; i < size; i += 2)
            {
                DateTime punchInTime = lstInOutPunch[i].H01F03;
                DateTime punchOutTime = lstInOutPunch[i + 1].H01F03;
                TimeSpan timeDiff = punchOutTime.Subtract(punchInTime);

                tempWorkHour += timeDiff.TotalHours;

                if ((i + 2 == size) || (lstInOutPunch[i].H01F02 != lstInOutPunch[i + 2].H01F02))
                {
                    lstAttendance.Add(new ATD01
                    {
                        D01F02 = lstInOutPunch[i].H01F02,
                        //D01F03 = _attendanceProcessingData.dateOfAttendance,
                        D01F03 = lstInOutPunch[i].H01F03.Date,
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
    }
}