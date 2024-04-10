using FirmAdvanceDemo.Models;
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
    public class BLLeave : BLResource<LVE01>
    {
        /// <summary>
        /// Method to add a leave to database
        /// </summary>
        /// <param name="leave">Leave instance</param>
        /// <returns>ResponseStatusInfo instance containing null</returns>
        public static ResponseStatusInfo AddLeave(LVE01 leave)
        {
            try
            {
                leave.e01f03 = DateTime.Now.Date;
                leave.e01f07 = LeaveStatus.Pending;

                if (leave.e01f03 > leave.e01f04)
                {
                    throw new Exception("Cannot reuqest leave for previous dates");
                }
                if (leave.e01f05 < 1)
                {
                    throw new Exception("Reuqest valid leave days");
                }
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // check that reuest leave is not coinciding with already reuested leave for the employee
                    DateTime startLeaveRqDate = leave.e01f04;
                    DateTime lastLeaveRqDate = leave.e01f04.AddDays(leave.e01f05 - 1);

                    SqlExpression<LVE01> sqlExp = db.From<LVE01>()
                        .Where(l => l.e01f02 == leave.e01f02)
                        .And(
                            "{0} >= e01f04 AND  {0} <= ADDDATE(e01f04, INTERVAL (e01f05 - 1) DAY)",
                            startLeaveRqDate.ToString("yyyy-MM-dd")
                          )
                        .Or(
                            "{0} >= e01f04 AND  {0} <= ADDDATE(e01f04, INTERVAL (e01f05 - 1) DAY)",
                            lastLeaveRqDate.ToString("yyyy-MM-dd")
                          )
                        .Or(l => l.e01f04 >= startLeaveRqDate && l.e01f04 <= lastLeaveRqDate)
                        .Select(Sql.Count("*"));

                    int count = db.Single<int>(sqlExp);

                    if (count > 0)
                    {
                        throw new Exception($"Employee with employee id: {leave.e01f02} is already is on leave on {leave.e01f04}");
                    }

                    db.Insert<LVE01>(leave);

                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Leave request for date: {DateTime.Now.Date} submitted for employee id: {leave.e01f02}",
                        Data = null
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

        /// <summary>
        /// Method to fetch leave of specified type (If leave type is LeaveStatus.None then it get all type of leaves)
        /// </summary>
        /// <param name="leaveType">leave type of Enum LeaveStatus</param>
        /// <returns>ResponseStatusInfo instance containing list_of_leaves, null when an exception occurs</returns>
        public static ResponseStatusInfo FetchLeaves(LeaveStatus leaveType)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    SqlExpression<LVE01> sqlExp = db.From<LVE01>();
                    if(leaveType != LeaveStatus.None)
                    {
                        sqlExp.Where(l => l.e01f07 == leaveType);
                    }

                    List<LVE01> lstResource = db.Select<LVE01>(sqlExp);

                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Existing {leaveType}",
                        Data = new { Resources = lstResource }
                    };
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
        }

        /// <summary>
        /// Method to get leave count of an employee (employee id) and the month-year
        /// </summary>
        /// <param name="EmployeeId">Employee id</param>
        /// <param name="month">Leave Month</param>
        /// <param name="year">Leave Year</param>
        /// <returns>ResponseStatusInfo instance containing leave count, null when an exception occurs</returns>
        public static ResponseStatusInfo GetLeaveCountByEmployeeIdAnMonthYear(int EmployeeId, int month, int year)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    DateTime MonthFirstDate = new DateTime(year, month, 1);
                    DateTime MonthLastDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                    SqlExpression<LVE01> sqlExp = db.From<LVE01>()
                        .Where(l => l.e01f02 == EmployeeId && l.e01f07 == LeaveStatus.Approved)
                        .And(l => l.e01f04 >= MonthFirstDate && l.e01f04 <= MonthLastDate)
                        .Or(
                            "(ADDDATE(e01f04, INTERVAL (e01f05 - 1) DAY) >= {0} AND ADDDATE(e01f04, INTERVAL (e01f05 - 1) DAY) <= {1})",
                            MonthFirstDate.ToString("yyyy-MM-dd"),
                            MonthLastDate.ToString("yyyy-MM-dd")
                     );
                    sqlExp.SelectExpression = $"sum(CASE WHEN e01f04 < '{MonthFirstDate.ToString("yyyy-MM-dd")}' THEN DATEDIFF(ADDDATE(e01f04, INTERVAL(e01f05 - 1) DAY), '{MonthFirstDate.ToString("yyyy-MM-dd")}') + 1 WHEN ADDDATE(e01f04, INTERVAL(e01f05 - 1) DAY) > '{MonthLastDate.ToString("yyyy-MM-dd")}' THEN DATEDIFF(e01f04, '{MonthLastDate.ToString("yyyy-MM-dd")}') + 1 ELSE DATEDIFF(ADDDATE(e01f04, INTERVAL(e01f05 - 1) DAY), e01f04) + 1 END) AS Leave_Count";

                    int leaveCount = db.Scalar<int>(sqlExp);

                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Leave count for employee with employeeId: {EmployeeId} for monht/year: {month}/{year}",
                        Data = new { LeaveCount = leaveCount }
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

        /// <summary>
        /// Method to fetch all leave of specified employee (by employee id)
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>ResponseStatusInfo instance containing list_of_leaves, null when an exception occurs</returns>
        public static ResponseStatusInfo FetchLeaveByEmployeeId(int EmployeeId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    List<LVE01> lstLeaveByEmployeeId = db.Select<LVE01>(Leave => Leave.e01f02 == EmployeeId);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Leave list of employee with employeeId: {EmployeeId}",
                        Data = lstLeaveByEmployeeId
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

        /// <summary>
        /// Method to fetch leave by month-year of leave
        /// </summary>
        /// <param name="month">Leave Month</param>
        /// <param name="year">Leave Year</param>
        /// <returns>ResponseStatusInfo instance containing list_of_leaves, null when an exception occurs                                                                                                         </returns>
        public static ResponseStatusInfo FetchLeaveByMonthYear(int month, int year)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    DateTime MonthFirstDate = new DateTime(year, month, 1);
                    DateTime MonthLastDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                    SqlExpression<LVE01> sqlExp = db.From<LVE01>().
                        Where(l => l.e01f07 == LeaveStatus.Approved)
                        .And(l => l.e01f04 >= MonthFirstDate && l.e01f04 <= MonthLastDate)
                        .Or(
                            "adddate(e01f04, INTERVAL (e01f05 - 1) DAY) >= {0} AND adddate(e01f04, INTERVAL (e01f05 - 1) DAY) <= {1}",
                            MonthFirstDate.ToString("yyyy-MM-dd"),
                            MonthLastDate.ToString("yyyy-MM-dd")
                        );

                    List<LVE01> lstLeaveByMonthYear = db.Select<LVE01>(sqlExp);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Leave list of employees for month/year: {month}/{year}",
                        Data = lstLeaveByMonthYear
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

        /// <summary>
        /// Method to fetch leaves on specified date
        /// </summary>
        /// <param name="date">Date for which leaves are to be fetched</param>
        /// <returns>ResponseStatusInfo instance containing list_of_leaves, null when an exception occurs</returns>
        public static ResponseStatusInfo FetchDateLeaves(DateTime? date = null)
        {
            date = date ?? DateTime.Now;
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<LVE01> sqlExp = db.From<LVE01>()
                        .Where(l => l.e01f07 == LeaveStatus.Approved)
                        .And(l => l.e01f04 <= date)
                        .And(
                            "adddate(e01f04, INTERVAL (e01f05 - 1) DAY) >= {1}",
                            LeaveStatus.Approved,
                            ((DateTime)date).ToString("yyyy-MM-dd")
                        );

                    List<LVE01> lstTodaysLeave = db.Select<LVE01>(sqlExp);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Leaves for date: {((DateTime)date).ToString("yyyy-MM-dd")}",
                        Data = lstTodaysLeave
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

        /// <summary>
        /// Method to fetch leave of an employee for month-year
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <param name="month">Leave Month</param>
        /// <param name="year">Year month</param>
        /// <returns></returns>
        public static ResponseStatusInfo FetchLeaveByEmployeeIdAndMonthYear(int EmployeeId, int month, int year)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    DateTime MonthFirstDate = new DateTime(year, month, 1);
                    DateTime MonthLastDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                    SqlExpression<LVE01> sqlExp = db.From<LVE01>()
                        .Where(leave => leave.e01f02 == EmployeeId)
                        .And(l => l.e01f07 == LeaveStatus.Approved)
                        .And(l => l.e01f04 >= MonthFirstDate && l.e01f04 <= MonthLastDate)
                        .Or(
                            "adddate(e01f04, INTERVAL (e01f05 - 1) DAY) >= {0} AND adddate(e01f04, INTERVAL (e01f05 - 1) DAY) <= {1}",
                            MonthFirstDate.ToString("yyyy-MM-dd"),
                            MonthLastDate.ToString("yyyy-MM-dd")
                        );

                    List<LVE01> lstLeaveByEmployeeIdAndMonthYear = db.Select<LVE01>(sqlExp);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Leave list of Employee with employeeId: {EmployeeId} for month/year: {month}/{year}",
                        Data = lstLeaveByEmployeeIdAndMonthYear
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

        /// <summary>
        /// Method to fetch leave of employee for current month
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>ResponseStatusInfo instance containing list_of_leaves, null when an exception occurs</returns>
        public static ResponseStatusInfo FetchLeaveByEmployeeIdForCurrentMonth(int EmployeeId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    DateTime CurrentDate = DateTime.Now;
                    DateTime CurrentMonthFirstDate = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
                    DateTime CurrentMonthLastDate = new DateTime(CurrentDate.Year, CurrentDate.Month, DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month));

                    SqlExpression<LVE01> sqlExp = db.From<LVE01>()
                        .Where(Leave => Leave.e01f02 == EmployeeId)
                        .And(l => l.e01f07 == LeaveStatus.Approved)
                        .And(l => l.e01f04 >= CurrentMonthFirstDate && l.e01f04 <= CurrentMonthLastDate)
                        .Or(
                            "adddate(e01f04, INTERVAL (e01f05 - 1) DAY) >= {0} AND adddate(e01f04, INTERVAL (e01f05 - 1) DAY) <= {1}",
                            CurrentMonthFirstDate.ToString("yyyy-MM-dd"),
                            CurrentMonthLastDate.ToString("yyyy-MM-dd")
                        );

                    List<LVE01> lstLeaveByEmployeeIdForCurrentMonth = db.Select<LVE01>(sqlExp);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Leave list of Employee with employeeId: {EmployeeId} for current month",
                        Data = lstLeaveByEmployeeIdForCurrentMonth
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

        /// <summary>
        /// Method to udpate leave status
        /// </summary>
        /// <param name="LeaveId">Leave Id</param>
        /// <param name="toChange">Leave Status to change</param>
        /// <returns>ResponseStatusInfo instance containing null</returns>
        public static ResponseStatusInfo UpdateLeaveStatus(int LeaveId, LeaveStatus toChange)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // get leave date and check if it is >= today
                    DateTime CurrentDate = DateTime.Today;

                    LVE01 leave = db.SingleById<LVE01>(LeaveId);
                    if(leave == null)
                    {
                        throw new Exception($"No leave exists with leave id: {LeaveId}");
                    }
                    if(leave.e01f07 == toChange)
                    {
                        throw new Exception($"Leave with leave id: {LeaveId} already {toChange}");
                    }
                    if(leave.e01f07 == LeaveStatus.Expired)
                    {
                        throw new Exception($"Leave with leave id: {LeaveId} expired cannot approve");
                    }
                    if(leave.e01f07 == LeaveStatus.Pending && leave.e01f04 < CurrentDate)
                    {
                        toChange = LeaveStatus.Expired;
                    }
                    db.Update<LVE01>(new { e01f07 = toChange }, l => l.Id == LeaveId);
                }

                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = true,
                    Message = $"Leave {toChange} for leaveId: {LeaveId}",
                    Data = null
                };
            }
            catch(Exception ex)
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// Method to change LeaveStatus to Expired if leave status is pending and it has been expired
        /// </summary>
        /// <returns>ResponseStatusInfo instance containing null</returns>
        public static ResponseStatusInfo UpdateLeavesToExpire()
        {
            try
            {
                // get leave date and check if it is >= today
                DateTime CurrentDate = DateTime.Today;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Update<LVE01>(new { e01f07 = LeaveStatus.Expired }, l => l.e01f07 == LeaveStatus.Pending && l.e01f04 < CurrentDate);
                }

                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = true,
                    Message = $"Pending leaves before {CurrentDate.ToString("yyyy-MM-dd")} marked as expired",
                    Data = null
                };
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
    }
}