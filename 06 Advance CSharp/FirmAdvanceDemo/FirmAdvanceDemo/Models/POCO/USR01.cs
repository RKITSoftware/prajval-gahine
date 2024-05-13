using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// User POCO model
    /// </summary>
    public class USR01
    {
        #region Public Properties
        /// <summary>
        /// user id
        /// </summary>
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// username
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        public string R01F04 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        public string R01F05 { get; set; }

        /// <summary>
        /// User creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime R01F06 { get; set; }

        /// <summary>
        /// User last update datetime
        /// </summary
        [IgnoreOnInsert]
        public DateTime? R01F07 { get; set; }
        #endregion
    }
}
