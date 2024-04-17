using FirmAdvanceDemo.Enums;
using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Punch POCO model
    /// </summary>
    public class PCH01 : IModel
    {
        /// <summary>
        /// Punch Id
        /// </summary>
        [PrimaryKey]
        public int P01F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        public int H01F02 { get; set; }

        /// <summary>
        /// Punch Type
        /// </summary>
        [IgnoreOnInsert]
        public EnmPunchType H01F03 { get; set; }

        /// <summary>
        /// Punch creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime H01F04 { get; set; }

        /// <summary>
        /// Punch last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime H01F05 { get; set; }
    }
}
