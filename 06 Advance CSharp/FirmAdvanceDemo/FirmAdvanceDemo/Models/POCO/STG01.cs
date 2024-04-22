using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Setting POCO model
    /// </summary>
    public class STG01
    {
        /// <summary>
        /// Primary key for STG01 (ORMLite forces first field as PK if not specified)
        /// </summary>
        [PrimaryKey]
        public int G01F01 { get; set; }

        /// <summary>
        /// Previous Month Salary CreditDate
        /// </summary>
        public DateTime G01F02 { get; set; }

        /// <summary>
        /// Setting creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime G01F03 { get; set; }

        /// <summary>
        /// Setting last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? G01F04 { get; set; }
    }
}
