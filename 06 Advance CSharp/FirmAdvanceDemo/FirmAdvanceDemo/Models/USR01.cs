using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models
{
    public class USR01 : IModel
    {
        /// <summary>
        /// user id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("r01f01")]
        public int Id { get; set; }

        /// <summary>
        /// username
        /// </summary>
        [Unique]
        [ValidateNotNull]
        [Index]
        public string r10f02 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        [ValidateNotNull]
        public byte[] r01f03 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        [ValidateNotNull]
        public string r01f04 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        [ValidateNotNull]
        public string r01f05 { get; set; }

        /// <summary>
        /// User creation date
        /// </summary>
        [ValidateNotNull]
        public DateTime r01f06 { get; set; }
    }
}