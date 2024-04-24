using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using static FirmAdvanceDemo.Utility.Constants;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Handles business logic for ATD01 operations.
    /// </summary>
    public class BLATD01Handler
    {
        /// <summary>
        /// The ATD01 object.
        /// </summary>
        private ATD01 _objATD01;

        /// <summary>
        /// The OrmLiteConnectionFactory object.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// The DBATD01Context object.
        /// </summary>
        private readonly DBATD01Context _objDBATD01Context;

        /// <summary>
        /// The DBPCH01Context object.
        /// </summary>
        private DBPCH01Context _objDBPCH01Context;

        /// <summary>
        /// The operation to be performed.
        /// </summary>
        public EnmOperation Operation { get; set; }

        /// <summary>
        /// Initializes a new instance of the BLATD01Handler class.
        /// </summary>
        public BLATD01Handler()
        {
            _objATD01 = new ATD01();
            _dbFactory = OrmliteDbConnector.DbFactory;
            _objDBATD01Context = new DBATD01Context();
        }

        /// <summary>
        /// Converts DTOATD01 DTO model to ATD01 POCO model
        /// </summary>
        /// <param name="objDTOATD01">The DTO object.</param>
        public void Presave(DTOATD01 objDTOATD01)
        {
            _objATD01 = objDTOATD01.ConvertModel<ATD01>();
            if (Operation == EnmOperation.A)
            {
                _objATD01.D01F01 = 0;
                _objATD01.D01F05 = DateTime.Now;
            }
            else
            {
                _objATD01.D01F06 = DateTime.Now;
            }
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

        /// <summary>
        /// Prevalidates the DTO object.
        /// </summary>
        /// <param name="objDTOATD01">The DTO object.</param>
        /// <returns>The response object.</returns>
        public Response Prevalidate(DTOATD01 objDTOATD01)
        {
            Response response = new Response();

            // check items employeeId and body employeeId are same?
            int employeeIdFromClaims = (int)HttpContext.Current.Items["employeeId"];
            int employeeId = objDTOATD01.D01F02;

            if (employeeIdFromClaims != employeeId)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.BadGateway;
                response.Message = "Employee ID in request body does not match the authenticated employee ID.";

                return response;
            }

            // check if employeeId exists in db?
            int count;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                count = (int)db.Count<EMP01>(employee => employee.P01F01 == employeeId);
            }

            if (count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Employee not found with employee id: {employeeId}";

                return response;
            }

            return response;
        }

        /// <summary>
        /// Processes end of day punches for a given date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The response object.</returns>
        public Response ProcessEndOfDayPunches(DateTime date)
        {
            Response response = new Response();

            _objDBPCH01Context = new DBPCH01Context();  // initalize DBPCH01Context

            // get list of unprocessed punches
            List<PCH01> lstPunch = _objDBPCH01Context.GetUnprocessedPunchesForDate(DateTime.Now);

            if (lstPunch.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No unprocessed punch found for date: {date.ToString(GlobalDateFormat)}";

                return response;
            }

            UpdatePunchType(lstPunch);  // update punch types

            List<ATD01> lstAttendance = ComputeAttendance(lstPunch);    // compute attendance

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction txn = db.OpenTransaction())
                {
                    try
                    {
                        db.UpdateAll<PCH01>(lstPunch);
                        db.InsertAll<ATD01>(lstAttendance);
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
            response.Message = $"Attendance evaluated for date: {date.ToString(GlobalDateFormat)}";
            return response;
        }

        /// <summary>
        /// Updates the punch type based on certain criteria.
        /// </summary>
        /// <param name="lstPunch">The list of punches to process.</param>
        private void UpdatePunchType(List<PCH01> lstPunch)
        {
            MarkMistakenlyPunch(lstPunch);
            MarkAmbiguousPunch(lstPunch);
            MarkInOutPunch(lstPunch);
        }

        /// <summary>
        /// Marks punches that are mistakenly recorded.
        /// </summary>
        /// <param name="lstPunch">The list of punches to process.</param>
        private void MarkMistakenlyPunch(List<PCH01> lstPunch)
        {
            TimeSpan timeBuffer = new TimeSpan(0, 0, 10);    // 1 minute buffer
            for (int i = 0; i < lstPunch.Count; i++)
            {
                if (i != 0 && lstPunch[i - 1].H01F02 == lstPunch[i].H01F02)
                {
                    if (lstPunch[i].H01F04.Subtract(lstPunch[i - 1].H01F04) <= timeBuffer)
                    {
                        //lstPunch.RemoveAt(i);
                        lstPunch[i].H01F03 = EnmPunchType.M;
                    }
                }
            }
        }

        /// <summary>
        /// Marks punches that are ambiguous or unclear.
        /// </summary>
        /// <param name="lstPunch">The list of punches to process.</param>
        private void MarkAmbiguousPunch(List<PCH01> lstPunch)
        {
            if (lstPunch == null || lstPunch.Count == 0)
            {
                return;
            }
            if (lstPunch.Count == 1)
            {
                lstPunch[0].H01F03 = EnmPunchType.A;
                return;
            }
            int size = lstPunch.Count;

            int tempEmployeeId = 0;
            int tempEmployeeStartIndex = 0;
            int tempEmployeelLegitimatePunchCount = 0;

            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    tempEmployeeId = lstPunch[0].H01F02;
                    tempEmployeelLegitimatePunchCount++;
                }
                else if (lstPunch[i].H01F03 != EnmPunchType.M && lstPunch[i].H01F02 == tempEmployeeId)
                {
                    tempEmployeelLegitimatePunchCount++;
                }

                // check if next punch is of different employee
                if (i == size - 1 || lstPunch[i].H01F02 != lstPunch[i + 1].H01F02)
                {
                    if (tempEmployeelLegitimatePunchCount % 2 != 0)
                    {
                        for (int j = tempEmployeeStartIndex; j <= i; j++)
                        {
                            if (lstPunch[j].H01F03 == EnmPunchType.U)
                            {
                                lstPunch[j].H01F03 = EnmPunchType.A;
                            }
                        }
                    }

                    // set up things for next employee punch(es)
                    if (i != size - 1)
                    {
                        tempEmployeeId = lstPunch[i + 1].H01F02;
                        tempEmployeeStartIndex = i + 1;
                        tempEmployeelLegitimatePunchCount = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Marks punches as In/Out based on certain criteria.
        /// </summary>
        /// <param name="lstPunch">The list of punches to process.</param>
        private void MarkInOutPunch(List<PCH01> lstPunch)
        {
            if (lstPunch == null || lstPunch.Count == 0)
            {
                return;
            }

            int size = lstPunch.Count;
            bool tempIsPunchedIn = false;
            for (int i = 0; i < size; i++)
            {
                if
                (lstPunch[i].H01F03 != EnmPunchType.M
                    && lstPunch[i].H01F03 != EnmPunchType.A)
                {
                    if (tempIsPunchedIn == false)
                    {
                        lstPunch[i].H01F03 = EnmPunchType.I;
                        tempIsPunchedIn = true;
                    }
                    else
                    {
                        lstPunch[i].H01F03 = EnmPunchType.O;
                        tempIsPunchedIn = false;
                    }
                }
            }
        }

        /// <summary>
        /// Computes attendance records based on the list of punches.
        /// </summary>
        /// <param name="lstPunch">The list of punches to process.</param>
        /// <returns>The computed attendance records.</returns>
        private List<ATD01> ComputeAttendance(List<PCH01> lstPunch)
        {
            if (lstPunch == null || lstPunch.Count == 0 || lstPunch.Count == 1)
            {
                return null;
            }

            List<ATD01> lstAttendance = new List<ATD01>();

            DateTime date = lstPunch[0].H01F04.Date;
            DateTime now = DateTime.Now;

            int size = lstPunch.Count;
            double tempWorkHpur = 0;

            int lastPunchInIndex = -1;
            for (int i = 0; i < size; i++)
            {
                if (lstPunch[i].H01F03 == EnmPunchType.I)
                {
                    lastPunchInIndex = i;
                }
                else if (lstPunch[i].H01F03 == EnmPunchType.O)
                {
                    tempWorkHpur += lstPunch[i].H01F04.Subtract(lstPunch[lastPunchInIndex].H01F04).TotalHours;
                }

                // check if next employee punch is of different employee ?
                if (tempWorkHpur > 0 && (i == size - 1 || lstPunch[i].H01F02 != lstPunch[i + 1].H01F02))
                {
                    int EmployeeId = lstPunch[i].H01F02;
                    lstAttendance.Add(
                            new ATD01
                            {
                                D01F02 = EmployeeId,
                                D01F03 = date,
                                D01F04 = tempWorkHpur,
                                D01F05 = now
                            }
                        );
                    tempWorkHpur = 0;
                }
            }

            if (lstAttendance.Count == 0)
            {
                return null;
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
        /// Validates the ATD01 POCO model for insert or update.
        /// </summary>
        /// <returns>The response object.</returns>
        public Response Validate()
        {
            Response response = new Response();
            // nothing to validate as of now
            return response;
        }

        /// <summary>
        /// Saves the ATD01 POCO model for insert or update.
        /// </summary>
        /// <returns>The response object.</returns>
        public Response Save()
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    int attendanceID = (int)db.Insert<ATD01>(_objATD01, selectIdentity: true);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Attendance {attendanceID} created.";
                }
                else
                {
                    db.Update<ATD01>(_objATD01);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Attendance {_objATD01.D01F01} updated.";
                }
            }
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