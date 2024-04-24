using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Business logic class responsible for downloading salary slips.
    /// </summary>
    public class BLDownload
    {
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Initializes a new instance of the BLDownload class.
        /// </summary>
        public BLDownload()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
        }

        /// <summary>
        /// Downloads the salary slip for a specific employee within a specified date range.
        /// </summary>
        /// <param name="EmployeeId">The ID of the employee for whom the salary slip is being downloaded.</param>
        /// <param name="start">The start date of the salary slip period.</param>
        /// <param name="end">The end date of the salary slip period.</param>
        /// <returns>A response containing the downloaded salary slip data in CSV format.</returns>
        public Response DownloadSalarySlip(int EmployeeId, DateTime start, DateTime end)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                // Construct the SQL expression to fetch salary data for the specified employee and date range
                SqlExpression<SLY01> sqlExp = db.From<SLY01>()
                    .Where(salary => salary.Y01F02 == EmployeeId)
                    .And(salary => salary.Y01F05 >= start && salary.Y01F05 <= end);

                // Execute the query and retrieve the salary data
                List<SLY01> lstSalary = db.Select<SLY01>(sqlExp);

                // Convert the salary data to CSV format
                string csvContent = GeneralUtility.ConvertToCSV<SLY01>(lstSalary);

                // Create and return a response containing the downloaded salary slip data
                return new Response
                {
                    IsError = true,
                    Message = $"Salary Slip of Employee Id {EmployeeId} from {start:dd-MM-yyyy} to {end:dd-MM-yyyy}",
                    Data = csvContent
                };
            }
        }
    }
}
