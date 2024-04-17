using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

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

        private OrmLiteConnectionFactory _dbFactory;

        private DBLVE02Context _objDBLVE02Context;

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
            using(IDbConnection db = _dbFactory.OpenDbConnection())
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
            if(Operation == EnmOperation.E)
            {
                int leaveId = objDTOLVE02.E02F01;
                int leaveCount;
                using(IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    leaveCount = (int)db.Count<LVE02>(leave => leave.E02F01 == leaveId);
                }
                if(leaveCount == 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Leave not found for id: {leaveId}";

                    return response;
                }
            }

            return response;
        }

        /// <summary>
        /// Method to convert DTOLVE02 instance to LVE02 instance
        /// </summary>
        /// <param name="objDTOLVE02">Instance of DTOLVE02</param>
        private void Presave(DTOLVE02 objDTOLVE02)
        {
            _objLVE02 = objDTOLVE02.ConvertModel<LVE02>();
            if(Operation == EnmOperation.A)
            {
                _objLVE02.E02F01 = 0;
                _objLVE02.E02F06 = EnmLeaveStatus.P;
                _objLVE02.E02F07 = DateTime.Now;
            }
            else
            {
                _objLVE02.E02F08 = DateTime.Now;
            }
        }

        public Response Validate()
        {
            Response response = new Response();

            // validate if leave already requested for given leave date range
            bool isLeaveConflict = IsLeaveConflict();

        }

        public bool IsLeaveConflict()
        {
            DateTime startDateTime = _objLVE02.E02F03;
            DateTime endDateTime = startDateTime.AddDays(_objLVE02.E02F04);

            int conflictCount = _objDBLVE02Context.FetchLeaveConflictCount(startDateTime, endDateTime);
        }

        /// <summary>
        /// Method to add a leave to database
        /// </summary>
        /// <param name="leave">Leave instance</param>
        /// <returns>ResponseStatusInfo instance containing null</returns>
        public Response AddLeave(LVE02 leave)
        {
            try
            {
                leave.E02F07 = DateTime.Now.Date;
                leave.E02F06 = EnmLeaveStatus.P;

                if (leave.E02F04 < 1)
                {
                    throw new Exception("Reuqest valid leave days");
                }
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // check that reuest leave is not coinciding with already reuested leave for the employee
                    DateTime startLeaveRqDate = leave.E02F03;
                    DateTime lastLeaveRqDate = leave.E02F03.AddDays(leave.E02F04 - 1);

                    SqlExpression<LVE02> sqlExp = db.From<LVE02>()
                        .Where(l => l.E02F02 == leave.E02F02)
                        .And(
                            "{0} >= E01F04 AND  {0} <= ADDDATE(E01F04, INTERVAL (E01F05 - 1) DAY)",
                            startLeaveRqDate.ToString("yyyy-MM-dd")
                          )
                        .Or(
                            "{0} >= E01F04 AND  {0} <= ADDDATE(E01F04, INTERVAL (E01F05 - 1) DAY)",
                            lastLeaveRqDate.ToString("yyyy-MM-dd")
                          )
                        .Or(l => l.E02F03 >= startLeaveRqDate && l.E02F03 <= lastLeaveRqDate)
                        .Select(Sql.Count("*"));

                    int count = db.Single<int>(sqlExp);

                    if (count > 0)
                    {
                        throw new Exception($"Employee with employee id: {leave.E02F02} is already is on leave on {leave.E02F03}");
                    }

                    db.Insert<LVE02>(leave);

                    return new Response()
                    {
                        IsError = true,
                        Message = $"Leave request for date: {DateTime.Now.Date} submitted for employee id: {leave.E02F02}",
                        Data = null
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
        /// Method to fetch leave of specified type (If leave type is LeaveStatus.None then it get all type of leaves)
        /// </summary>
        /// <param name="leaveType">leave type of Enum LeaveStatus</param>
        /// <returns>ResponseStatusInfo instance containing list_of_leaves, null when an exception occurs</returns>
        public Response FetchLeaves(EnmLeaveStatus leaveType)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    SqlExpression<LVE02> sqlExp = db.From<LVE02>();
                    if (leaveType != EnmLeaveStatus.N)
                    {
                        sqlExp.Where(l => l.E02F06 == EnmLeaveStatus.A);
                    }

                    List<LVE02> lstResource = db.Select<LVE02>(sqlExp);

                    return new Response()
                    {
                        IsError = true,
                        Message = $"Existing {leaveType}",
                        Data = new { Resources = lstResource }
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
        }

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
                    sqlExp.SelectExpression = $"sum(CASE WHEN E01F04 < '{MonthFirstDate.ToString("yyyy-MM-dd")}' THEN DATEDIFF(ADDDATE(E01F04, INTERVAL(E01F05 - 1) DAY), '{MonthFirstDate.ToString("yyyy-MM-dd")}') + 1 WHEN ADDDATE(E01F04, INTERVAL(E01F05 - 1) DAY) > '{MonthLastDate.ToString("yyyy-MM-dd")}' THEN DATEDIFF(E01F04, '{MonthLastDate.ToString("yyyy-MM-dd")}') + 1 ELSE DATEDIFF(ADDDATE(E01F04, INTERVAL(E01F05 - 1) DAY), E01F04) + 1 END) AS Leave_Count";

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
        public Response FetchDateLeaves(DateTime? date = null)
        {
            date = date ?? DateTime.Now;
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<LVE02> sqlExp = db.From<LVE02>()
                        .Where(l => l.E02F06 == EnmLeaveStatus.A)
                        .And(l => l.E02F03 <= date)
                        .And(
                            "adddate(E01F04, INTERVAL (E01F05 - 1) DAY) >= {1}",
                            EnmLeaveStatus.A,
                            ((DateTime)date).ToString("yyyy-MM-dd")
                        );

                    List<LVE02> lstTodaysLeave = db.Select<LVE02>(sqlExp);
                    return new Response()
                    {
                        IsError = true,
                        Message = $"Leaves for date: {((DateTime)date).ToString("yyyy-MM-dd")}",
                        Data = lstTodaysLeave
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
        /// <param name="LeaveId">Leave Id</param>
        /// <param name="toChange">Leave Status to change</param>
        /// <returns>ResponseStatusInfo instance containing null</returns>
        public Response UpdateLeaveStatus(int LeaveId, EnmLeaveStatus toChange)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // get leave date and check if it is >= today
                    DateTime CurrentDate = DateTime.Today;

                    LVE02 leave = db.SingleById<LVE02>(LeaveId);
                    if (leave == null)
                    {
                        throw new Exception($"No leave exists with leave id: {LeaveId}");
                    }
                    if (leave.E02F06 == toChange)
                    {
                        throw new Exception($"Leave with leave id: {LeaveId} already {toChange}");
                    }
                    if (leave.E02F06 == EnmLeaveStatus.E)
                    {
                        throw new Exception($"Leave with leave id: {LeaveId} expired cannot approve");
                    }
                    if (leave.E02F06 == EnmLeaveStatus.P && leave.E02F03 < CurrentDate)
                    {
                        toChange = EnmLeaveStatus.E;
                    }
                    db.Update<LVE02>(new { E01F07 = toChange }, l => l.E02F02 == LeaveId);
                }

                return new Response()
                {
                    IsError = true,
                    Message = $"Leave {toChange} for leaveId: {LeaveId}",
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
                    Message = $"Pending leaves before {CurrentDate.ToString("yyyy-MM-dd")} marked as expired",
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
    }
}