using FirmAdvanceDemo.Enums;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

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
        [Alias("d01f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        public int h01f02 { get; set; }

        /// <summary>
        /// DateTime of Punch
        /// </summary>
        public DateTime h01f03 { get; set; }

        /// <summary>
        /// Punch Type
        /// </summary>
        [IgnoreOnInsert]
        public EnmPunchType h01f04 { get; set; }

        /// <summary>
        /// Punch creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime h01f05 { get; set; }

        /// <summary>
        /// Punch last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime h01f06 { get; set; }
    }
}
