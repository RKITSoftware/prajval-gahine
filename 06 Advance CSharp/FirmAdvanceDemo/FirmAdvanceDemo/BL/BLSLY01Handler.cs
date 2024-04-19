using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Business logic class for Salary - defines all props and methods to suppor Salary controller
    /// </summary>
    public class BLSLY01Handler
    {
        /// <summary>
        /// Instance of SLY01 model
        /// </summary>
        private SLY01 _objSLY01;

        private DBSLY01Context _dBSLY01Context;

        protected readonly OrmLiteConnectionFactory _dbFactory;

        public BLSLY01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _dBSLY01Context = new DBSLY01Context();
            _objSLY01 = new SLY01();
        }

        /// <summary>
        /// Method to credit employees salary based on their attendance from last_salary_credit_date <= date < current_date
        /// </summary>
        /// <returns>ResponseStatusInfo instance containing data as null>/returns>
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
            if(lastCreditDate.Year == now.Year && lastCreditDate.Month == now.Month)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Conflict;
                response.Message = "Salary for the current month has already been processed.";

                return response;
            }

            // from attendance table fetch attendances from [last credit salary date] till [yesterday] and aggragte sum on workhour for each employeeId filtered.
            DataTable dtEmployeeWorkHour = _dBSLY01Context.FetchUnpaidWorkHours(lastCreditDate);
            if(dtEmployeeWorkHour.Rows.Count == 0)
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
                            G01F01 = 1,
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

        private List<SLY01> EmployeeWorkHourToSLY01(DataTable dtEmployeeWorkHour)
        {
            DateTime now = DateTime.Now;
            double totalHoursInCurrentMonth = DateTime.DaysInMonth(now.Year, now.Month) * 24;
            List<SLY01> lstSalary = new List<SLY01>(dtEmployeeWorkHour.Rows.Count);

            for(int i = 0; i < lstSalary.Count; i++)
            {
                DataRow row = dtEmployeeWorkHour.Rows[i];

                lstSalary[i].Y01F02 = (int)row["EmployeeId"];
                lstSalary[i].Y01F05 = now;

                // calculate salary using workhour and monthly salary
                double monthlySalary = (double)row["MonthlySalary"];
                double workHours = (double)row["WorkHours"];
                double salaryAmount = (workHours / totalHoursInCurrentMonth) * monthlySalary;

                lstSalary[i].Y01F03 = salaryAmount;
                lstSalary[i].Y01F04 = (int)row["PositionId"];
            }

            return lstSalary;
        }
    }
}