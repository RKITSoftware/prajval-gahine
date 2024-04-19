using FirmAdvanceDemo.Enums;
using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Leave POCO model
    /// </summary>
    public class LVE02
    {
        /// <summary>
        /// Leave Id
        /// </summary>
        [PrimaryKey]
        public int E02F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [IgnoreOnUpdate]
        public int E02F02 { get; set; }

        /// <summary>
        /// Leave Datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime E02F03 { get; set; }

        /// <summary>
        /// No. of leaves from Leave Date
        /// </summary>
        [IgnoreOnUpdate]
        public int E02F04 { get; set; }

        /// <summary>
        /// Reason for leave
        /// </summary>
        public string E02F05 { get; set; }

        /// <summary>
        /// Leave status
        /// </summary>
        [IgnoreOnInsert]
        public EnmLeaveStatus E02F06 { get; set; }

        /// <summary>
        /// Leave creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime E02F07 { get; set; }

        /// <summary>
        /// Leave last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime E02F08 { get; set; }
    }
}
