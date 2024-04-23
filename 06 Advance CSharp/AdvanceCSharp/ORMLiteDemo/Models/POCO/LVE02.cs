using ORMLiteDemo.Enums;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace ORMLiteDemo.Models.POCO
{
    /// <summary>
    /// Leave POCO model
    /// </summary>
    public class LVE02
    {
        /// <summary>
        /// Leave Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int E02F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [IgnoreOnUpdate]
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        public int E02F02 { get; set; }

        /// <summary>
        /// Leave Datetime
        /// </summary>
        [IgnoreOnUpdate]
        [ValidateNotNull]
        public DateTime E02F03 { get; set; }

        /// <summary>
        /// No. of leaves from Leave Date
        /// </summary>
        [IgnoreOnUpdate]
        [ValidateNotNull]
        public int E02F04 { get; set; }

        /// <summary>
        /// Reason for leave
        /// </summary>
        [ValidateNotNull]
        public string E02F05 { get; set; }

        /// <summary>
        /// Leave status
        /// </summary>
        public EnmLeaveStatus E02F06 { get; set; }

        /// <summary>
        /// Leave creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        [ValidateNotNull]
        public DateTime E02F07 { get; set; }

        /// <summary>
        /// Leave last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? E02F08 { get; set; }
    }
}
