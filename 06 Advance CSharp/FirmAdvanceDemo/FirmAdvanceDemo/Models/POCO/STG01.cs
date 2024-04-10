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
        [AutoIncrement]
        [Alias("g01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Previous Month Salary CreditDate
        /// </summary>
        public DateTime? g01f02 { get; set; }

        /// <summary>
        /// Setting creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime g01f03 { get; set; }

        /// <summary>
        /// Setting last modified datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime g01f04 { get; set; }
    }
}