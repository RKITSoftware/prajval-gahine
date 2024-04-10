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
        [JsonPropertyName("r01102")]
        public string r01f02 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        [ValidateNotNull]
        public byte[] r01f03 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("r01104")]
        public string r01f04 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("r01105")]
        public string r01f05 { get; set; }

        /// <summary>
        /// User creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime r01f06 { get; set; }

        /// <summary>
        /// User last update datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime r01f07 { get; set; }
    }
}
