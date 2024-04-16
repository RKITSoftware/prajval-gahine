using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// User POCO model
    /// </summary>
    public class USR01 : IModel
    {
        /// <summary>
        /// user id
        /// </summary>
        [PrimaryKey]
        [Alias("r01f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// username
        /// </summary>
        public string r01f02 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        public byte[] r01f03 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        public string r01f04 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        public string r01f05 { get; set; }

        /// <summary>
        /// User creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime r01f06 { get; set; }

        /// <summary>
        /// User last update datetime
        /// </summary
        [IgnoreOnInsert]
        public DateTime r01f07 { get; set; }
    }
}
