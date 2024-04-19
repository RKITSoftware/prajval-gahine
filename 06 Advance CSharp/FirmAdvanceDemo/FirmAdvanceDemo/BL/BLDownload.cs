using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;

namespace FirmAdvanceDemo.BL
{
    public class BLDownload
    {
        private readonly OrmLiteConnectionFactory _dbFactory;
        public BLDownload()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
        }
        public Response DownloadSalarySlip(int EmployeeId, DateTime start, DateTime end)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                SqlExpression<SLY01> sqlExp = db.From<SLY01>()
                    .Where(salary => salary.Y01F02 == EmployeeId)
                    .And(salary => salary.Y01F05 >= start && salary.Y01F05 <= end);

                List<SLY01> lstSalary = db.Select<SLY01>(sqlExp);

                string csvContent = GeneralUtility.ConvertToCSV<SLY01>(lstSalary, typeof(SLY01));


                return new Response
                {
                    IsError = true,
                    Message = $"Salary Slip of Employee Id {EmployeeId} from {start:dd-MM-yyy} to {end:dd-MM-yyy}",
                    Data = csvContent
                };
            }
        }
    }
}