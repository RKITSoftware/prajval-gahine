using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Business logic class for Leave - defines all props and methods to support Leave controller
    /// </summary>
    public class BLLVE02Handler : BLResource<LVE02>
    {
        /// <summary>
        /// Instance of LVE02 model
        /// </summary>
        private LVE02 _objLVE02;

        /// <summary>
        /// Default constructor for BLLeave, initializes LVE02 instance
        /// </summary>
        public BLLVE02Handler()
        {
            _objLVE02 = new LVE02();
        }

        /// <summary>
        /// Method to convert DTOLVE02 instance to LVE02 instance
        /// </summary>
        /// <param name="objDTOLVE02">Instance of DTOLVE02</param>
        private void Presave(DTOLVE02 objDTOLVE02)
        {
            _objLVE02 = objDTOLVE02.ConvertModel<LVE02>();
        }

        /// <summary>
        /// Method to validate the LVE02 instance
        /// </summary>
        /// <returns>True if LVE02 instance is valid else false</returns>
        private bool Validate()
        {
            return true;
        }

        /// <summary>
        /// Method to Add (Create) a new record of lve02 table in DB
        /// </summary>
        private void Add()
        {

        }

        /// <summary>
        /// Method to Update (Modify) an existing record lve02 table in DB
        /// </summary>
        private void Update()
        {

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
                leave.e02f03 = DateTime.Now.Date;
                leave.e02f07 = LeaveStatus.P;

                if (leave.e02f03 > leave.e02f04)
                {
                    throw new Exception("Cannot reuqest leave for previous dates");
                }
                if (leave.e02f05 < 1)
                {
                    throw new Exception("Reuqest valid leave days");
                }
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // check that reuest leave is not coinciding with already reuested leave for the employee
                    DateTime startLeaveRqDate = leave.e02f04;
                    DateTime lastLeaveRqDate = leave.e02f04.AddDays(leave.e02f05 - 1);

                    SqlExpression<LVE02> sqlExp = db.From<LVE02>()
                        .Where(l => l.e02f02 == leave.e02f02)
                        .And(
                            "{0} >= e01f04 AND  {0} <= ADDDATE(e01f04, INTERVAL (e01f05 - 1) DAY)",
                            startLeaveRqDate.ToString("yyyy-MM-dd")
                          )
                        .Or(
                            "{0} >= e01f04 AND  {0} <= ADDDATE(e01f04, INTERVAL (e01f05 - 1) DAY)",
                            lastLeaveRqDate.ToString("yyyy-MM-dd")
                          )
                        .Or(l => l.e02f04 >= startLeaveRqDate && l.e02f04 <= lastLeaveRqDate)
                        .Select(Sql.Count("*"));

                    int count = db.Single<int>(sqlExp);

                    if (count > 0)
                    {
                        throw new Exception($"Employee with employee id: {leave.e02f02} is already is on leave on {leave.e02f04}");
                    }

                    db.Insert<LVE02>(leave);

                    return new Response()
                    {
                        IsError = true,
                        Message = $"Leave request for date: {DateTime.Now.Date} submitted for employee id: {leave.e02f02}",
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
        public Response FetchLeaves(LeaveStatus leaveType)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    SqlExpression<LVE02> sqlExp = db.From<LVE02>();
                    if (leaveType != LeaveStatus.N)
                    {
                        sqlExp.Where(l => l.e02f07 == LeaveStatus.A);
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
                        .Where(l => l.e02f02 == EmployeeId && l.e02f07 == LeaveStatus.A)
                        .And(l => l.e02f04 >= MonthFirstDate && l.e02f04 <= MonthLastDate)
                        .Or(
                            "(ADDDATE(e01f04, INTERVAL (e01f05 - 1) DAY) >= {0} AND ADDDATE(e01f04, INTERVAL (e01f05 - 1) DAY) <= {1})",
                            MonthFirstDate.ToString("yyyy-MM-dd"),
                            MonthLastDate.ToString("yyyy-MM-dd")
                     );
                    sqlExp.SelectExpression = $"sum(CASE WHEN e01f04 < '{MonthFirstDate.ToString("yyyy-MM-dd")}' THEN DATEDIFF(ADDDATE(e01f04, INTERVAL(e01f05 - 1) DAY), '{MonthFirstDate.ToString("yyyy-MM-dd")}') + 1 WHEN ADDDATE(e01f04, INTERVAL(e01f05 - 1) DAY) > '{MonthLastDate.ToString("yyyy-MM-dd")}' THEN DATEDIFF(e01f04, '{MonthLastDate.ToString("yyyy-MM-dd")}') + 1 ELSE DATEDIFF(ADDDATE(e01f04, INTERVAL(e01f05 - 1) DAY), e01f04) + 1 END) AS Leave_Count";

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
                    List<LVE02> lstLeaveByEmployeeId = db.Select<LVE02>(Leave => Leave.e02f02 == EmployeeId);
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
                        Where(l => l.e02f07 == LeaveStatus.A)
                        .And(l => l.e02f04 >= MonthFirstDate && l.e02f04 <= MonthLastDate)
                        .Or(
                            "adddate(e01f04, INTERVAL (e01f05 - 1) DAY) >= {0} AND adddate(e01f04, INTERVAL (e01f05 - 1) DAY) <= {1}",
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
                        .Where(l => l.e02f07 == LeaveStatus.A)
                        .And(l => l.e02f04 <= date)
                        .And(
                            "adddate(e01f04, INTERVAL (e01f05 - 1) DAY) >= {1}",
                            LeaveStatus.A,
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
                        .Where(leave => leave.e02f02 == EmployeeId)
                        .And(l => l.e02f07 == LeaveStatus.A)
                        .And(l => l.e02f04 >= MonthFirstDate && l.e02f04 <= MonthLastDate)
                        .Or(
                            "adddate(e01f04, INTERVAL (e01f05 - 1) DAY) >= {0} AND adddate(e01f04, INTERVAL (e01f05 - 1) DAY) <= {1}",
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
                        .Where(Leave => Leave.e02f02 == EmployeeId)
                        .And(l => l.e02f07 == LeaveStatus.A)
                        .And(l => l.e02f04 >= CurrentMonthFirstDate && l.e02f04 <= CurrentMonthLastDate)
                        .Or(
                            "adddate(e01f04, INTERVAL (e01f05 - 1) DAY) >= {0} AND adddate(e01f04, INTERVAL (e01f05 - 1) DAY) <= {1}",
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
        public Response UpdateLeaveStatus(int LeaveId, LeaveStatus toChange)
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
                    if (leave.e02f07 == toChange)
                    {
                        throw new Exception($"Leave with leave id: {LeaveId} already {toChange}");
                    }
                    if (leave.e02f07 == LeaveStatus.E)
                    {
                        throw new Exception($"Leave with leave id: {LeaveId} expired cannot approve");
                    }
                    if (leave.e02f07 == LeaveStatus.P && leave.e02f04 < CurrentDate)
                    {
                        toChange = LeaveStatus.E;
                    }
                    db.Update<LVE02>(new { e01f07 = toChange }, l => l.p01f01 == LeaveId);
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
                    db.Update<LVE02>(new { e01f07 = LeaveStatus.E }, l => l.e02f07 == LeaveStatus.P && l.e02f04 < CurrentDate);
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