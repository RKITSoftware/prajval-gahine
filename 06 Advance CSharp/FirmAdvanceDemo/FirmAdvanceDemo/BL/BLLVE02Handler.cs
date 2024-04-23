using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using static FirmAdvanceDemo.Utitlity.Constants;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Business logic class for Leave - defines all props and methods to support Leave controller
    /// </summary>
    public class BLLVE02Handler
    {
        /// <summary>
        /// Instance of LVE02 model
        /// </summary>
        private LVE02 _objLVE02;

        private readonly OrmLiteConnectionFactory _dbFactory;

        private readonly DBLVE02Context _objDBLVE02Context;

        public EnmOperation Operation;

        /// <summary>
        /// Default constructor for BLLeave, initializes LVE02 instance
        /// </summary>
        public BLLVE02Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _objDBLVE02Context = new DBLVE02Context();
        }

        public Response Prevalidate(DTOLVE02 objDTOLVE02)
        {
            Response response = new Response();

            // check employee ID in either A or E
            int employeeId = objDTOLVE02.E02F02;
            int employeeCount;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                employeeCount = (int)db.Count<EMP01>(employee => employee.P01F01 == employeeId);
            }

            if (employeeCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Employee not found with id: {employeeId}";

                return response;
            }

            // check leave ID in case of E
            if (Operation == EnmOperation.E)
            {
                int leaveID = objDTOLVE02.E02F01;
                int leaveCount;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    leaveCount = (int)db.Count<LVE02>(leave => leave.E02F01 == leaveID);
                }
                if (leaveCount == 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Leave not found for id: {leaveID}";

                    return response;
                }

                // if employee is editing,
                // then he cannot edit non-pending status leave
                EnmLeaveStatus leaveStatus;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    leaveStatus = db.Scalar<LVE02, EnmLeaveStatus>(leave => leave.E02F01 == objDTOLVE02.E02F01);
                }

                if (leaveStatus != EnmLeaveStatus.P)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Conflict;
                    response.Message = $"Leave {objDTOLVE02.E02F01} is not in pending state.";

                    return response;
                }
            }

            return response;
        }

        /// <summary>
        /// Method to convert DTOLVE02 instance to LVE02 instance
        /// </summary>
        /// <param name="objDTOLVE02">Instance of DTOLVE02</param>
        public void Presave(DTOLVE02 objDTOLVE02)
        {
            _objLVE02 = objDTOLVE02.ConvertModel<LVE02>();

            // set employeeID in case of employee is accessing
            if (!GeneralUtility.IsAdmin())
            {
                _objLVE02.E02F02 = GeneralUtility.GetEmployeeIDFromItems();
            }

            if (Operation == EnmOperation.A)
            {
                _objLVE02.E02F01 = 0;
                _objLVE02.E02F03 = _objLVE02.E02F03.Date;
                _objLVE02.E02F06 = EnmLeaveStatus.P;
                _objLVE02.E02F07 = DateTime.Now;
            }
            else
            {
                // if employee is editing then override status to P
                if (!GeneralUtility.IsAdmin())
                {
                    _objLVE02.E02F06 = EnmLeaveStatus.P;
                }
                _objLVE02.E02F08 = DateTime.Now;
            }
        }

        public Response Validate()
        {
            Response response = new Response();

            // nothing to validate as of now
            return response;
        }

        public Response Save()
        {
            Response response = new Response();

            if (Operation == EnmOperation.A)
            {
                if (!_objDBLVE02Context.RequestLeave(_objLVE02))
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Conflict;
                    response.Message = "Leave request already exists within the specified range.";

                    return response;
                }

                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objLVE02);
                }

                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Leave request submitted.";

                return response;
            }
            else
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Update<LVE02>(_objLVE02);
                }
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Leave: {_objLVE02.E02F01} updated";

                return response;
            }
        }

        //check378 - get leave by leave status

        /// <summary>
        /// Method to get leave count of an employee (employee id) and the month-year
        /// </summary>
        /// <param name="EmployeeId">Employee id</param>
        /// <param name="month">Leave Month</param>
        /// <param name="year">Leave Year</param>
        /// <returns>ResponseStatusInfo instance containing leave count, null when an exception occurs</returns>
        public Response GetLeaveCountByEmployeeIdAnMonthYear(int EmployeeId, int month, int year)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    DateTime MonthFirstDate = new DateTime(year, month, 1);
                    DateTime MonthLastDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                    SqlExpression<LVE02> sqlExp = db.From<LVE02>()
                        .Where(l => l.E02F02 == EmployeeId && l.E02F06 == EnmLeaveStatus.A)
                        .And(l => l.E02F03 >= MonthFirstDate && l.E02F03 <= MonthLastDate)
                        .Or(
                            "(ADDDATE(E01F04, INTERVAL (E01F05 - 1) DAY) >= {0} AND ADDDATE(E01F04, INTERVAL (E01F05 - 1) DAY) <= {1})",
                            MonthFirstDate.ToString("yyyy-MM-dd"),
                            MonthLastDate.ToString("yyyy-MM-dd")
                     );
                    string v = $"sum(CASE WHEN E01F04 < '{MonthFirstDate:yyyy-MM-dd}' THEN DATEDIFF(ADDDATE(E01F04, INTERVAL(E01F05 - 1) DAY), '{MonthFirstDate:yyyy-MM-dd}') + 1 WHEN ADDDATE(E01F04, INTERVAL(E01F05 - 1) DAY) > '{MonthLastDate:yyyy-MM-dd}' THEN DATEDIFF(E01F04, '{MonthLastDate:yyyy-MM-dd}') + 1 ELSE DATEDIFF(ADDDATE(E01F04, INTERVAL(E01F05 - 1) DAY), E01F04) + 1 END) AS Leave_Count";
                    sqlExp.SelectExpression = v;

                    int leaveCount = db.Scalar<int>(sqlExp);

                    return new Response()
                    {
                        IsError = true,
                        Message = $"Leave count for employee with employeeId: {EmployeeId} for monht/year: {month}/{year}",
                        Data = new { LeaveCount = leaveCount }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsError = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// Method to fetch all leave of specified employee (by employee id)
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>ResponseStatusInfo instance containing list_of_leaves, null when an exception occurs</returns>
        public Response FetchLeaveByEmployeeId(int EmployeeId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    List<LVE02> lstLeaveByEmployeeId = db.Select<LVE02>(Leave => Leave.E02F02 == EmployeeId);
                    return new Response()
                    {
                        IsError = true,
                        Message = $"Leave list of employee with employeeId: {EmployeeId}",
                        Data = lstLeaveByEmployeeId
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsError = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// Method to fetch leave by month-year of leave
        /// </summary>
        /// <param name="month">Leave Month</param>
        /// <param name="year">Leave Year</param>
        /// <returns>ResponseStatusInfo instance containing list_of_leaves, null when an exception occurs                                                                                                         </returns>
        public Response FetchLeaveByMonthYear(int month, int year)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    DateTime MonthFirstDate = new DateTime(year, month, 1);
                    DateTime MonthLastDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                    SqlExpression<LVE02> sqlExp = db.From<LVE02>().
                        Where(l => l.E02F06 == EnmLeaveStatus.A)
                        .And(l => l.E02F03 >= MonthFirstDate && l.E02F03 <= MonthLastDate)
                        .Or(
                            "adddate(E01F04, INTERVAL (E01F05 - 1) DAY) >= {0} AND adddate(E01F04, INTERVAL (E01F05 - 1) DAY) <= {1}",
                            MonthFirstDate.ToString("yyyy-MM-dd"),
                            MonthLastDate.ToString("yyyy-MM-dd")
                        );

                    List<LVE02> lstLeaveByMonthYear = db.Select<LVE02>(sqlExp);
                    return new Response()
                    {
                        IsError = true,
                        Message = $"Leave list of employees for month/year: {month}/{year}",
                        Data = lstLeaveByMonthYear
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsError = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// Method to fetch leaves on specified date
        /// </summary>
        /// <param name="date">Date for which leaves are to be fetched</param>
        /// <returns>ResponseStatusInfo instance containing list_of_leaves, null when an exception occurs</returns>


        /// <summary>
        /// Method to fetch leave of an employee for month-year
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <param name="month">Leave Month</param>
        /// <param name="year">Year month</param>
        /// <returns></returns>
        public Response FetchLeaveByEmployeeIdAndMonthYear(int EmployeeId, int month, int year)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    DateTime MonthFirstDate = new DateTime(year, month, 1);
                    DateTime MonthLastDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                    SqlExpression<LVE02> sqlExp = db.From<LVE02>()
                        .Where(leave => leave.E02F02 == EmployeeId)
                        .And(l => l.E02F06 == EnmLeaveStatus.A)
                        .And(l => l.E02F03 >= MonthFirstDate && l.E02F03 <= MonthLastDate)
                        .Or(
                            "adddate(E01F04, INTERVAL (E01F05 - 1) DAY) >= {0} AND adddate(E01F04, INTERVAL (E01F05 - 1) DAY) <= {1}",
                            MonthFirstDate.ToString("yyyy-MM-dd"),
                            MonthLastDate.ToString("yyyy-MM-dd")
                        );

                    List<LVE02> lstLeaveByEmployeeIdAndMonthYear = db.Select<LVE02>(sqlExp);
                    return new Response()
                    {
                        IsError = true,
                        Message = $"Leave list of Employee with employeeId: {EmployeeId} for month/year: {month}/{year}",
                        Data = lstLeaveByEmployeeIdAndMonthYear
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsError = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// Method to fetch leave of employee for current month
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>ResponseStatusInfo instance containing list_of_leaves, null when an exception occurs</returns>
        public Response FetchLeaveByEmployeeIdForCurrentMonth(int EmployeeId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    DateTime CurrentDate = DateTime.Now;
                    DateTime CurrentMonthFirstDate = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
                    DateTime CurrentMonthLastDate = new DateTime(CurrentDate.Year, CurrentDate.Month, DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month));

                    SqlExpression<LVE02> sqlExp = db.From<LVE02>()
                        .Where(Leave => Leave.E02F02 == EmployeeId)
                        .And(l => l.E02F06 == EnmLeaveStatus.A)
                        .And(l => l.E02F03 >= CurrentMonthFirstDate && l.E02F03 <= CurrentMonthLastDate)
                        .Or(
                            "adddate(E01F04, INTERVAL (E01F05 - 1) DAY) >= {0} AND adddate(E01F04, INTERVAL (E01F05 - 1) DAY) <= {1}",
                            CurrentMonthFirstDate.ToString("yyyy-MM-dd"),
                            CurrentMonthLastDate.ToString("yyyy-MM-dd")
                        );

                    List<LVE02> lstLeaveByEmployeeIdForCurrentMonth = db.Select<LVE02>(sqlExp);
                    return new Response()
                    {
                        IsError = true,
                        Message = $"Leave list of Employee with employeeId: {EmployeeId} for current month",
                        Data = lstLeaveByEmployeeIdForCurrentMonth
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsError = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// Method to udpate leave status
        /// </summary>
        /// <param name="leaveID">Leave Id</param>
        /// <param name="toChange">Leave Status to change</param>
        /// <returns>ResponseStatusInfo instance containing null</returns>
        public Response UpdateLeaveStatus(int leaveID, EnmLeaveStatus toChange)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // get leave date and check if it is >= today
                    DateTime CurrentDate = DateTime.Today;

                    LVE02 leave = db.SingleById<LVE02>(leaveID);
                    if (leave == null)
                    {
                        throw new Exception($"No leave exists with leave id: {leaveID}");
                    }
                    if (leave.E02F06 == toChange)
                    {
                        throw new Exception($"Leave with leave id: {leaveID} already {toChange}");
                    }
                    if (leave.E02F06 == EnmLeaveStatus.E)
                    {
                        throw new Exception($"Leave with leave id: {leaveID} expired cannot approve");
                    }
                    if (leave.E02F06 == EnmLeaveStatus.P && leave.E02F03 < CurrentDate)
                    {
                        toChange = EnmLeaveStatus.E;
                    }
                    db.Update<LVE02>(new { E01F07 = toChange }, l => l.E02F02 == leaveID);
                }

                return new Response()
                {
                    IsError = true,
                    Message = $"Leave {toChange} for leaveID: {leaveID}",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsError = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// Method to change LeaveStatus to Expired if leave status is pending and it has been expired
        /// </summary>
        /// <returns>ResponseStatusInfo instance containing null</returns>
        public Response UpdateLeavesToExpire()
        {
            try
            {
                // get leave date and check if it is >= today
                DateTime CurrentDate = DateTime.Today;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Update<LVE02>(new { E01F07 = EnmLeaveStatus.E }, l => l.E02F06 == EnmLeaveStatus.P && l.E02F03 < CurrentDate);
                }

                return new Response()
                {
                    IsError = true,
                    Message = $"Pending leaves before {CurrentDate:yyyy-MM-dd} marked as expired",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsError = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public Response RetrieveLeave()
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeave();
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No leaves found";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        public Response RetrieveLeave(int leaveID)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeave(leaveID);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Leave: {leaveID} not found";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        public Response RetrieveLeaveByStatus(EnmLeaveStatus leaveStatus)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveByStatus(leaveStatus);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No leaves found";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        public Response RetrieveLeaveByEmployee(int employeeId)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveByEmployee(employeeId);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No leaves found for employee: {employeeId}";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        public Response ValidateDelete(int leaveID)
        {
            Response response = new Response();

            int count;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                count = (int)db.Count<LVE02>(leave => leave.E02F01 == leaveID);
            }
            if (count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Leave with ID {leaveID} not found.";

                return response;
            }

            // if employee then check employeeID associated with leaveID
            if (!GeneralUtility.IsAdmin())
            {
                int leaveEmployeeID;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    leaveEmployeeID = db.Scalar<LVE02, int>(leave => leave.E02F02, leave => leave.E02F01 == leaveID);
                }
                if (!GeneralUtility.IsAuthorizedEmployee(_objLVE02.E02F02))
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Conflict;
                    response.Message = $"You are not authorized to access employee {leaveEmployeeID}.";

                    return response;
                }
            }
            return response;
        }

        public Response Delete(int leaveID)
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<LVE02>(leaveID);
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Leave with ID {leaveID} deleted.";

            return response;
        }

        public Response RetrieveLeaveByMonthYear(int year, int month)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveByMonthYear(year, month);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No leaves found for month: {year}/{month}.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        public Response RetrieveLeaveByDate(DateTime date)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveByDate(date);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No leaves found for date: {date.ToString(GlobalDateFormat)}.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        public Response RetrieveLeaveByEmployeeAndMonthYear(int employeeId, int year, int month)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveByEmployeeAndMonthYear(employeeId, year, month);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No leaves found for employee {employeeId} for {year}/{month}.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }

        public Response RetrieveLeaveByEmployeeAnYear(int employeeId, int year)
        {
            Response response = new Response();
            DataTable dtLeave = _objDBLVE02Context.FetchLeaveByEmployeeAndMonth(employeeId, year);
            if (dtLeave.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No leaves found for employee {employeeId} for {year}.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtLeave;

            return response;
        }
    }
}