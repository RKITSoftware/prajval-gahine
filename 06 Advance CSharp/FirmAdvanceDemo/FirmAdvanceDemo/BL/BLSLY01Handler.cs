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
        /// Instance of SLY01 model.
        /// </summary>
        private readonly SLY01 _objSLY01;

        /// <summary>
        /// Context for Salary handler.
        /// </summary>
        private readonly DBSLY01Context _dBSLY01Context;

        /// <summary>
        /// OrmLite Connection Factory instance representing a connection with a particular database.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Default constructor for BLSLY01Handler.
        /// </summary>
        public BLSLY01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _dBSLY01Context = new DBSLY01Context();
            _objSLY01 = new SLY01();
        }

        /// <summary>
        /// Credits employees' salary based on their attendance from last_salary_credit_date to current_date.
        /// </summary>
        /// <returns>A response indicating the outcome of the credit operation.</returns>
        public Response CreditSalary()
        {
            Response response = new Response();

            // get [last credit salary date]
            DateTime lastCreditDate;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lastCreditDate = db.Scalar<STG01, DateTime>(salary => salary.G01F02);
            }

            // if month same as current then error "Salary already credited for current month".
            DateTime now = DateTime.Now;
            if (now.Date <= lastCreditDate)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Conflict;
                response.Message = "Salary for the current month has already been processed.";

                return response;
            }

            // from attendance table fetch attendances from [last credit salary date] till [yesterday] and aggragte sum on workhour for each employeeId filtered.
            DataTable dtEmployeeWorkHour = _dBSLY01Context.FetchUnpaidWorkHours(lastCreditDate);
            if (dtEmployeeWorkHour.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Unable to process salary: no work hours recorded for the employee.";

                return response;
            }
            // now u have employee id and their workhour.

            List<SLY01> lstSalary = EmployeeWorkHourToSLY01(dtEmployeeWorkHour);

            // now insert into sly01 table accordingly and update setting table by modifying lst credit date
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction txn = db.OpenTransaction())
                {
                    try
                    {
                        db.InsertAll<SLY01>(lstSalary);
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
            response.Message = "Salary Credited";

            return response;
        }

        /// <summary>
        /// Converts employee work hour data from DataTable to a list of SLY01 instances.
        /// </summary>
        /// <param name="dtEmployeeWorkHour">DataTable containing employee work hour data.</param>
        /// <returns>A list of SLY01 instances representing the converted employee work hour data.</returns>
        private List<SLY01> EmployeeWorkHourToSLY01(DataTable dtEmployeeWorkHour)
        {
            DateTime now = DateTime.Now;
            int totalHoursInCurrentMonth = GeneralUtility.MonthTotalHoursWithoutWeekends(now.Year, now.Month);
            List<SLY01> lstSalary = new List<SLY01>(dtEmployeeWorkHour.Rows.Count);


            for (int i = 0; i < dtEmployeeWorkHour.Rows.Count; i++)
            {
                DataRow row = dtEmployeeWorkHour.Rows[i];

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
    }
}