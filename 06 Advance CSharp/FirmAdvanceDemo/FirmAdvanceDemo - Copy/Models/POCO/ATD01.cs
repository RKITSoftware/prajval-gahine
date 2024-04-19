using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    [CompositeIndex(unique: true, "d01f02", "d01f03")]
    public class ATD01
    {
        /// <summary>
        /// Attendance Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int D01F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        public int D01F02 { get; set; }

        /// <summary>
        /// Date of attendance
        /// </summary>
        [ValidateNotNull]
        public DateTime D01F03 { get; set; }

        /// <summary>
        /// Day work hour
        /// </summary>
        [ValidateNotNull]
        public double D01F04 { get; set; }

        /// <summary>
        /// Attendance creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        [ValidateNotNull]
        public DateTime D01F05 { get; set; }

        /// <summary>
        /// Attendance last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime D01F06 { get; set; }
    }
}