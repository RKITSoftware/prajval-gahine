using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Handles business logic related to salary operations.
    /// </summary>
    public class BLSLY01Handler
    {
        /// <summary>
        /// Context for Salary handler.
        /// </summary>
        private readonly DBSLY01Context _dBSLY01Context;

        /// <summary>
        /// OrmLite Connection Factory instance representing a connection with a particular database.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Represents the data needed for processing salary payments.
        /// </summary>
        private struct SalaryProcessingData
        {
            /// <summary>
            /// The date up to which the salary is to be credited.
            /// </summary>
            public DateTime UptoCreditDate;

            /// <summary>
            /// DataTable containing employee work hours until yesterday.
            /// </summary>
            public DataTable DtEmployeeWorkHoursUntilYesterday;

            /// <summary>
            /// List of salary entries to be paid.
            /// </summary>
            public List<SLY01> LstToBePaidSalary;
        }

        /// <summary>
        /// The data for processing salary payments.
        /// </summary>
        private SalaryProcessingData _objProcessSalaryData;

        /// <summary>
        /// Default constructor for BLSLY01Handler.
        /// </summary>
        public BLSLY01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _dBSLY01Context = new DBSLY01Context();
        }

        /// <summary>
        /// Pre-saves unsalaried attendance records.
        /// </summary>
        public void PresaveUnSalariedAttendance(int year, int month)
        {
            _objProcessSalaryData = new SalaryProcessingData();
            _objProcessSalaryData.UptoCreditDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            _objProcessSalaryData.DtEmployeeWorkHoursUntilYesterday = _dBSLY01Context.FetchUnpaidWorkHours(_objProcessSalaryData.UptoCreditDate);
            _objProcessSalaryData.LstToBePaidSalary = ProcessDtEmployeeWorkHour();
        }

        /// <summary>
        /// Validates unsalaried attendance records.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateUnSalariedAttendance()
        {
            Response response = new Utility.Response();
            if (_objProcessSalaryData.DtEmployeeWorkHoursUntilYesterday.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No pending salaries.";

                return response;
            }
            return response;
        }

        /// <summary>
        /// Saves salaries for employees.
        /// </summary>
        /// <returns>A response indicating the save result.</returns>
        public Response SaveSalaries()
        {
            Response response = new Response();
            DateTime now = DateTime.Now;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction txn = db.OpenTransaction())
                {
                    try
                    {
                        db.InsertAll<SLY01>(_objProcessSalaryData.LstToBePaidSalary);

                        // query to update salaried attendance
                        string query = string.Format(@"
                                    UPDATE
                                        atd01
                                    SET
                                        d01f05 = 1,
                                        d01f07 = '{1}'
                                    WHERE
                                        d01f05 = 0 AND
                                        d01f03 <= '{0}'",
                                    _objProcessSalaryData.UptoCreditDate.ToString(Constants.GlobalDateFormat),
                                    DateTime.Now.ToString(Constants.GlobalDateTimeFormat));
                        db.ExecuteNonQuery(query);

                        STG01 objSTG01 = new STG01()
                        {
                            G01F01 = 0,
                            G01F02 = _objProcessSalaryData.UptoCreditDate,
                            G01F04 = now
                        };
                        db.Update<STG01>(objSTG01);

                        txn.Commit();
                    }
                    catch
                    {
                        txn.Rollback();
                    }
                }
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Salaries credited till date {_objProcessSalaryData.UptoCreditDate.ToString(Constants.GlobalDateFormat)}.";
            return response;
        }

        /// <summary>
        /// Converts employee work hour data from DataTable to a list of SLY01 instances.
        /// </summary>
        /// <returns>A list of SLY01 instances representing the converted employee work hour data.</returns>
        private List<SLY01> ProcessDtEmployeeWorkHour()
        {
            DateTime now = DateTime.Now;
            int totalHoursInCurrentMonth = GeneralUtility.MonthTotalHoursWithoutWeekends(now.Year, now.Month);
            List<SLY01> lstSalary = new List<SLY01>(_objProcessSalaryData.DtEmployeeWorkHoursUntilYesterday.Rows.Count);

            for (int i = 0; i < _objProcessSalaryData.DtEmployeeWorkHoursUntilYesterday.Rows.Count; i++)
            {
                DataRow row = _objProcessSalaryData.DtEmployeeWorkHoursUntilYesterday.Rows[i];

                // calculate salary using workhour and monthly salary
                double monthlySalary = (double)row["MontlhySalary"];
                double workHours = (double)row["WorkHours"];
                double salaryAmount = (workHours / totalHoursInCurrentMonth) * monthlySalary;


                DateTime monthStartDate = new DateTime(_objProcessSalaryData.UptoCreditDate.Year, _objProcessSalaryData.UptoCreditDate.Month, 1);
                lstSalary.Add(new SLY01
                {
                    Y01F02 = (int)row["employeeID"],
                    Y01F03 = salaryAmount,
                    Y01F04 = monthStartDate,
                    Y01F05 = (int)row["PositionID"],
                    Y01F06 = now,
                });
            }

            return lstSalary;
        }

        /// <summary>
        /// Downloads the salary slip for the specified employee and date range.
        /// </summary>
        /// <param name="employeeID">The ID of the employee.</param>
        /// <param name="start">The start date of the salary slip period.</param>
        /// <param name="end">The end date of the salary slip period.</param>
        /// <returns>A response containing the downloaded salary slip data.</returns>
        public Response DownloadSalarySlip(int employeeID, DateTime start, DateTime end)
        {
            Response response = new Response();

            DataTable dtSalary = _dBSLY01Context.FetchSalaryByEmployeeForDateRange(employeeID, start, end);

            if (dtSalary.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No salary record found for employee {employeeID}";
                return response;
            }
            string employeeFullName = string.Empty;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                employeeFullName = db.Scalar<EMP01, string>(employee => employee.P01F02 + " " + employee.P01F03, employee => employee.P01F01 == employeeID).ToUpper();
            }
            string[] lstMainHeader = new string[] { string.Format("{0} - {1}", employeeID, employeeFullName) };

            string totalAmount = dtSalary.AsEnumerable()
                .Select(dt => (double)dt["Amount"])
                .Aggregate(0.0, (acc, curr) => acc + curr).ToString();

            string[] lstFooter = new string[] { "TOTAL", totalAmount };
            byte[] salaryCSV = GeneralUtility.ConvertToCSV(dtSalary, lstMainHeader, lstFooter);

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = salaryCSV;
            return response;
        }

        /// <summary>
        /// Downloads the salary slip for the specified month.
        /// </summary>
        /// <param name="year">The year of specified month.</param>
        /// <param name="month">The month of salary slip.</param>
        /// <returns>A response containing the downloaded salary slip data.</returns>
        public Response DownloadSalarySlipForMonth(int year, int month)
        {
            Response response = new Response();

            DataTable dtSalary = _dBSLY01Context.FetchSalaryForMonth(year, month);

            if (dtSalary.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No salary record found for {year}/{month}";
                return response;
            }

            string[] lstMainHeader = new string[] { string.Format("\"{0}, {1}\"", year, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)) };

            string totalAmount = dtSalary.AsEnumerable()
                .Select(dt => (double)dt["Amount"])
                .Aggregate(0.0, (acc, curr) => acc + curr).ToString();

            string[] lstFooter = new string[] { " ", " ", "TOTAL", totalAmount };

            byte[] salaryCSV = GeneralUtility.ConvertToCSV(dtSalary, lstMainHeader, lstFooter);

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = salaryCSV;
            return response;
        }
    }
}