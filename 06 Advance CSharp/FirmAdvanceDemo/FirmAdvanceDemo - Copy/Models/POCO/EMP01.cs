using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Employee POCO model
    /// </summary>
    public class EMP01
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int P01F01 { get; set; }

        /// <summary>
        /// Employee First Name
        /// </summary>
        [ValidateNotNull]
        public string P01F02 { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary>
        [ValidateNotNull]
        public string P01F03 { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        [ValidateNotNull]
        public char P01F04 { get; set; }

        /// <summary>
        /// Employee Date of Birth
        /// </summary>
        [ValidateNotNull]
        public DateTime P01F05 { get; set; }

        /// <summary>
        /// Position Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(PSN01), OnDelete = "CASCADE")]
        public int P01F06 { get; set; }

        /// <summary>
        /// Employee creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        [ValidateNotNull]
        public DateTime P01F07 { get; set; }

        /// <summary>
        /// Employee last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime P01F08 { get; set; }
    }
}
