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
            _dbFactory = Connection.DbFactory;
        }
        public ResponseStatusInfo DownloadSalarySlip(int EmployeeId, DateTime start, DateTime end)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                SqlExpression<SLY01> sqlExp = db.From<SLY01>()
                    .Where(salary => salary.y01f02 == EmployeeId)
                    .And(salary => salary.y01f03 >= start && salary.y01f03 <= end);

                List<SLY01> lstSalary = db.Select<SLY01>(sqlExp);

                string csvContent = CSVConvert<SLY01>.ConvertToCSV(lstSalary, typeof(SLY01));


                return new ResponseStatusInfo
                {
                    IsRequestSuccessful = true,
                    Message = $"Salary Slip of Employee Id {EmployeeId} from {start.ToString("dd-MM-yyy")} to {end.ToString("dd-MM-yyy")}",
                    Data = csvContent
                };
            }
        }
    }
}