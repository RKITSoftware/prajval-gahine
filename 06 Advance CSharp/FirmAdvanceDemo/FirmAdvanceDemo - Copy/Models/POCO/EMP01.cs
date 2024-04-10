using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    public class EMP01 : IModel
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("p01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Employee First Name
        /// </summary>
        [ValidateNotNull]
        public string p01f02 { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary>
        [ValidateNotNull]
        public string p01f03 { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        [ValidateNotNull]
        public char p01f04 { get; set; }

        /// <summary>
        /// Employee Date of Birth
        /// </summary>
        [ValidateNotNull]
        public DateTime p01f05 { get; set; }

        /// <summary>
        /// Position Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(PSN01), OnDelete = "CASCADE")]
        public int p01f06 { get; set; }
    }
}