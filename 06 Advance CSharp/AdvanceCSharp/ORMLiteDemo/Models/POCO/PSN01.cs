
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace ORMLiteDemo.Models.POCO
{
    /// <summary>
    /// Position POCO model
    /// </summary>
    public class PSN01
    {
        /// <summary>
        /// Position Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int N01F01 { get; set; }

        /// <summary>
        /// Position Name
        /// </summary>
        [ValidateNotNull]
        public string N01F02 { get; set; }

        /// <summary>
        /// Annual Package (LPA)
        /// </summary>
        [ValidateNotNull]
        public double N01F03 { get; set; }

        /// <summary>
        /// Monthly Salary
        /// </summary>
        [ValidateNotNull]
        public double N01F04 { get; set; }

        /// <summary>
        /// Yearly Bonus
        /// </summary>
        [ValidateNotNull]
        public double N01F05 { get; set; }

        /// <summary>
        /// Department Id
        /// </summary>
        [ForeignKey(typeof(DPT01), OnDelete = "CASCADE")]
        public int N01F06 { get; set; }

        /// <summary>
        /// Position creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        [ValidateNotNull]
        public DateTime N01F07 { get; set; }

        /// <summary>
        /// Position last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? N01F08 { get; set; }
    }
}
