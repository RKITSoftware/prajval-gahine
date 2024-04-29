using FirmAdvanceDemo.Enums;
using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Punch POCO model
    /// </summary>
    public class PCH01
    {
        /// <summary>
        /// Punch Id
        /// </summary>
        [PrimaryKey]
        public int H01F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [IgnoreOnUpdate]
        public int H01F02 { get; set; }

        /// <summary>
        /// Date time of punch
        /// </summary>
        public DateTime H01F03 { get; set; }

        /// <summary>
        /// Punch Type
        /// </summary>
        public EnmPunchType H01F04 { get; set; }

        /// <summary>
        /// Punch considered in attendance ?
        /// </summary>
        public bool H01F05 { get; set; }

        /// <summary>
        /// Punch creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime H01F06 { get; set; }

        /// <summary>
        /// Punch last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? H01F07 { get; set; }
    }
}
