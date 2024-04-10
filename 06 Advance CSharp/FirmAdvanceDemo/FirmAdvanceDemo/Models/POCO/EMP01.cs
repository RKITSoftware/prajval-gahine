using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Employee POCO model
    /// </summary>
    public class EMP01 : IModel
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("p01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Employee First Name
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("p01102")]
        public string p01f02 { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("p01103")]
        public string p01f03 { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("p01104")]
        public char p01f04 { get; set; }

        /// <summary>
        /// Employee Date of Birth
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("p01105")]
        public DateTime p01f05 { get; set; }

        /// <summary>
        /// Position Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(PSN01), OnDelete = "CASCADE")]
        [JsonPropertyName("p01106")]
        public int p01f06 { get; set; }

        /// <summary>
        /// Employee creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime p01f07 { get; set; }

        /// <summary>
        /// Employee last modified datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime p01f08 { get; set; }
    }
}
