using ORMLiteDemo.Enums;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace ORMLiteDemo.Models.POCO
{
    /// <summary>
    /// Punch POCO model
    /// </summary>
    public class PCH01
    {
        /// <summary>
        /// Punch Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int P01F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        public int H01F02 { get; set; }

        /// <summary>
        /// Punch Type
        /// </summary>
        [ValidateNotNull]
        [Default(typeof(EnmPunchType), "'U'")]
        public EnmPunchType H01F03 { get; set; }

        /// <summary>
        /// Punch creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        [ValidateNotNull]
        public DateTime H01F04 { get; set; }

        /// <summary>
        /// Punch last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? H01F05 { get; set; }
    }
}
