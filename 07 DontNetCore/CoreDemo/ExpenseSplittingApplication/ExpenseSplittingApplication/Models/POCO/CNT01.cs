using ServiceStack.DataAnnotations;
using System;

namespace ExpenseSplittingApplication.Models.POCO
{
    public class CNT01
    {
        /// <summary>
        /// User Expense Payee ID
        /// </summary>
        [PrimaryKey]
        public int T01F01 { get; set; }

        /// <summary>
        /// Expense ID
        /// </summary>
        public int T01F02 { get; set; }

        /// <summary>
        /// User ID (Payee)
        /// </summary>
        public int T01F03 { get; set; }

        /// <summary>
        /// Amount to be paid
        /// </summary>
        public double T01F04 { get; set; }

        /// <summary>
        /// Is paid
        /// </summary>
        public bool T01F05 { get; set; }

        /// <summary>
        /// User Expense Payee creation datetime
        /// </summary>
        public DateTime T01F98 { get; set; }

        /// <summary>
        /// User Expense Payee last modified datetime
        /// </summary>
        public DateTime? T01F99 { get; set; }
    }
}
