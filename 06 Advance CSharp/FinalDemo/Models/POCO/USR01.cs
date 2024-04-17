using ServiceStack.DataAnnotations;
using System;

namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// Represents a POCO for a user entity in the system.
    /// </summary>
    public class USR01
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the user account.
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime R01F04 { get; set; }

        [IgnoreOnInsert]
        /// <summary>
        /// Gets or sets the last modified date and time of the user account.
        /// </summary>
        public DateTime R01F05 { get; set; }

        #endregion
    }
}