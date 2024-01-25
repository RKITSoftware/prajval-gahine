using System;

namespace FinalDemo.Models
{
    public class LeaveRequest
    {
        /// <summary>
        /// RequestId
        /// </summary>
        public int RequestId { get; set; }

        /// <summary>
        /// EmployeeName
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// StartDate
        /// </summary>
        public Nullable<DateTime> StartDate { get; set; } = DateTime.Now;

        /// <summary>
        /// EndDate
        /// </summary>
        public Nullable<DateTime> EndDate { get; set; } = DateTime.Now.AddDays(1);

        /// <summary>
        /// Status (pending , approved, rejected)
        /// </summary>
        public string Status { get; set; } 
    }
}