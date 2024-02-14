using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Data;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Business logic class for Salary - defines all props and methods to suppor Salary controller
    /// </summary>
    public class BLSalary : BLResource<SLY01>
    {
        /// <summary>
        /// Method to credit employees salary based on their attendance from last_salary_credit_date <= date < current_date
        /// </summary>
        /// <returns>ResponseStatusInfo instance containing data as null>/returns>
        public static ResponseStatusInfo CreditSalary()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<STG01> sqlExp = db.From<STG01>();

                    db.ExecuteSql("CALL Salary_Credit()");

                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Salary credited for current month {DateTime.Now.ToString("yyyy-MM-dd")}",
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
    }
}