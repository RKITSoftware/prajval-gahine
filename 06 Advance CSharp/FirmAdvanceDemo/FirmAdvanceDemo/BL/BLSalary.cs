using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
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
        /// Instance of SLY01 model
        /// </summary>
        private SLY01 _objSLY01;

        /// <summary>
        /// Default constructor for BLSalary, initializes SLY01 instance
        /// </summary>
        public BLSalary()
        {
            _objSLY01 = new SLY01();
        }

        /// <summary>
        /// Method to convert DTOSLY01 instance to SLY01 instance
        /// </summary>
        /// <param name="objDTOSLY01">Instance of DTOSLY01</param>
        private void Presave(DTOSLY01 objDTOSLY01)
        {
            _objSLY01 = objDTOSLY01.ConvertModel<SLY01>();
        }

        /// <summary>
        /// Method to validate the SLY01 instance
        /// </summary>
        /// <returns>True if SLY01 instance is valid else false</returns>
        private bool Validate()
        {
            return true;
        }

        /// <summary>
        /// Method to Add (Create) a new record of sly01 table in DB
        /// </summary>
        private void Add()
        {

        }

        /// <summary>
        /// Method to Update (Modify) an existing record sly01 table in DB
        /// </summary>
        private void Update()
        {

        }

        /// <summary>
        /// Method to credit employees salary based on their attendance from last_salary_credit_date <= date < current_date
        /// </summary>
        /// <returns>ResponseStatusInfo instance containing data as null>/returns>
        public ResponseStatusInfo CreditSalary()
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