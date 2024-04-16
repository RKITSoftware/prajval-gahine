using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Setting POCO model
    /// </summary>
    public class STG01 : IModel
    {
        /// <summary>
        /// Primary key for STG01 (ORMLite forces first field as PK if not specified)
        /// </summary>
        [PrimaryKey]
        [Alias("g01f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// Previous Month Salary CreditDate
        /// </summary>
        public DateTime g01f02 { get; set; }

        /// <summary>
        /// Setting creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime g01f03 { get; set; }

        /// <summary>
        /// Setting last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime g01f04 { get; set; }
    }
}
