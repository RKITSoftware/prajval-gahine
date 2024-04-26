using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
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

        private struct SalaryProcessingData
        {
            /// <summary>
            /// DataTable containing employee work hours until yesterday.
            /// </summary>
            public DataTable DtEmployeeWorkHoursUntilYesterday;

            /// <summary>
            /// DataTable containing employee work hours until yesterday.
            /// </summary>
            public List<SLY01> LstToBePaidSalary;
        }

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
        public void PresaveUnSalariedAttendance()
        {
            _objProcessSalaryData = new SalaryProcessingData();
            _objProcessSalaryData.DtEmployeeWorkHoursUntilYesterday = _dBSLY01Context.FetchUnpaidWorkHours();
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
                                        d01f07 = 1
                                    WHERE
                                        d01f07 = 0 AND
                                        d01f03 < '{0}'",
                                    DateTime.Now.ToString(Constants.GlobalDateFormat));
                        db.ExecuteNonQuery(query);

                        STG01 objSTG01 = new STG01()
                        {
                            G01F01 = 0,
                            G01F02 = now.Date.AddDays(-1),
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
            response.Message = $"Salaries credited till date {now.ToString(Constants.GlobalDateFormat)}.";
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

                lstSalary.Add(new SLY01
                {
                    Y01F02 = (int)row["EmployeeID"],
                    Y01F03 = salaryAmount,
                    Y01F04 = (int)row["PositionID"],
                    Y01F05 = now,
                });
            }

            return lstSalary;
        }

        /// <summary>
        /// Converts a DataTable to a CSV byte array.
        /// </summary>
        /// <param name="dt">The DataTable to convert.</param>
        /// <returns>A byte array representing the CSV data.</returns>
        public Response DownloadSalarySlip(int EmployeeId, DateTime start, DateTime end)
        {
            Response response = new Response();

            DataTable dtSalary = _dBSLY01Context.FetchSalaryByEmployeeForDateRange(EmployeeId, start, end);

            if (dtSalary.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No salary record found for employee {EmployeeId}";
                return response;
            }

            byte[] salaryCSV = GeneralUtility.ConvertToCSV(dtSalary);

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = salaryCSV;
            return response;
        }
    }
}