using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace ORMLiteDemo.Models.POCO
{
    /// <summary>
    /// Salary POCO model
    /// </summary>
    public class SLY01
    {
        /// <summary>
        /// Salary Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int Y01F01 { get; set; }

        /// <summary>
        /// Employee Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        public int Y01F02 { get; set; }

        /// <summary>
        /// Salary Amount
        /// </summary>
        [ValidateNotNull]
        [IgnoreOnUpdate]
        public double Y01F03 { get; set; }

        /// <summary>
        /// Salary month
        /// </summary>
        public DateTime Y01F04 { get; set; }

        /// <summary>
        /// Position Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(PSN01), OnDelete = "CASCADE")]
        public int Y01F05 { get; set; }

        /// <summary>
        /// Salary creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        [ValidateNotNull]
        public DateTime Y01F06 { get; set; }

        /// <summary>
        /// Salary last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? Y01F07 { get; set; }
    }
}
