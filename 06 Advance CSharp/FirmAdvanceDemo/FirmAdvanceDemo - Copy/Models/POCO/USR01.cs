using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// User POCO model
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// user id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// username
        /// </summary>
        [Unique]
        [ValidateNotNull]
        [Index]
        public string R01F02 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        [ValidateNotNull]
        public string R01F03 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        [ValidateNotNull]
        public string R01F04 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        [ValidateNotNull]
        public string R01F05 { get; set; }

        /// <summary>
        /// User creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        [ValidateNotNull]
        public DateTime R01F06 { get; set; }

        /// <summary>
        /// User last update datetime
        /// </summary
        [IgnoreOnInsert]
        public DateTime R01F07 { get; set; }
    }
}
