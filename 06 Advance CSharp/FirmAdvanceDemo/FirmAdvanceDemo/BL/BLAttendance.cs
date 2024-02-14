using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Business logic class for Attendance - defines all props and methods to support Attendance controller
    /// </summary>
    public class BLAttendance : BLResource<ATD01>
    {
        /// <summary>
        /// Method to add an attendance to datatbase
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <param name="DayWorkHour">Current date work hour</param>
        /// <returns>ResponseStatusInfo instance containing null</returns>
        public static ResponseStatusInfo AddAttendance(int EmployeeId, double DayWorkHour)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    ATD01 attendance = new ATD01()
                    {
                        Id = -1,
                        d01f02 = EmployeeId,
                        d01f03 = DateTime.Now.Date,
                        d01f04 = DayWorkHour,
                    };
                    db.Insert<ATD01>(attendance);

                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Todays attendance for Employee with employeeId: {EmployeeId} filled successfully",
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
        /// Method to fetch all attendances of an employee
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>ResponseStatusInfo instance containing lst_of_attendance, null if any exception is thrown</returns>
        public static ResponseStatusInfo FetchAttendanceByEmployeeId(int EmployeeId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    List<ATD01> lstAttendanceByEmployeeId = db.Select<ATD01>(attendance => attendance.d01f02 == EmployeeId);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Attendance list of employee with employeeId: {EmployeeId}",
                        Data = lstAttendanceByEmployeeId
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
        /// Method to fetch all attendance of specified month-year
        /// </summary>
        /// <param name="month">Attendance Month</param>
        /// <param name="year">Attendance Year</param>
        /// <returns>ResponseStatusInfo instance containing lst_of_attendance, null if any exception is thrown</returns>
        public static ResponseStatusInfo FetchAttendanceByMonthYear(int month, int year)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<ATD01> sqlExp = db.From<ATD01>();
                    sqlExp.Where(
                            "(YEAR(d01f03)) = {0}",
                            year
                         )
                        .And(
                            "(MONTH(d01f03)) = {0}",
                            month
                         );

                    List<ATD01> lstAttendanceByMonthYear = db.Select<ATD01>(sqlExp);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Attendance list of employees for month/year: {month}/{year}",
                        Data = lstAttendanceByMonthYear
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
        /// Method to fetch all todays attendance
        /// </summary>
        /// <returns>ResponseStatusInfo instance containing lst_of_attendance, null if any exception is thrown</returns>
        public static ResponseStatusInfo FetchTodaysAttendance()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    DateTime CurrentDate = DateTime.Today;
                    SqlExpression<ATD01> sqlExp = db.From<ATD01>();
                    sqlExp.Where(
                            $"(YEAR(d01f03)) = {0}",
                            CurrentDate.Year
                        )
                        .And(
                            $"(MONTH(d01f03)) = {0}",
                            CurrentDate.Month
                        );


                    List<ATD01> lstTodaysAttendance = db.Select<ATD01>(sqlExp);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = "Todays attendance list",
                        Data = lstTodaysAttendance
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
        /// Method to fetch attendance by employee id and month-year
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        /// <returns>ResponseStatusInfo instance containing lst_of_attendance, null if any exception is thrown</returns>
        public static ResponseStatusInfo FetchAttendanceByEmployeeIdAndMonthYear(int EmployeeId, int month, int year)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<ATD01> sqlExp = db.From<ATD01>();
                    sqlExp
                        .Where(attendance => attendance.d01f02 == EmployeeId)
                        .And(
                            "(YEAR(d01f03)) = {0}",
                            year
                         )
                        .And(
                            "(MONTH(d01f03)) = {0}",
                            month
                         );

                    List<ATD01> lstAttendanceByEmployeeIdAndMonthYear = db.Select<ATD01>(sqlExp);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Attendance list of Employee with employeeId: {EmployeeId} for month/year: {month}/{year}",
                        Data = lstAttendanceByEmployeeIdAndMonthYear
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

        /*
        /// <summary>
        /// Method to fetch all attendance of employee for current month
        /// </summary>
        /// <param name="EmployeeId">Employee id</param>
        /// <returns>ResponseStatusInfo instance containing lst_of_attendance, null if any exception is thrown</returns>
        public static ResponseStatusInfo FetchAttendanceByEmployeeIdForCurrentMonth(int EmployeeId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {

                    DateTime CurrentDate = DateTime.Today;

                    SqlExpression<ATD01> sqlExp = db.From<ATD01>();
                    sqlExp.Where(attendance => attendance.d01f02 == EmployeeId)
                        .And(
                            "YEAR(d01f03) = {0}",
                            CurrentDate.Year
                        )
                        .And(
                            "MONTH(d01f03) = {0}",
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