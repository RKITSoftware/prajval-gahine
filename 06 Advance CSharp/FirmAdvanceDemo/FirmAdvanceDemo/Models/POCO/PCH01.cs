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
        [AutoIncrement]
        [PrimaryKey]
        [Alias("d01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        [JsonPropertyName("h01102")]
        public int h01f02 { get; set; }

        /// <summary>
        /// DateTime of Punch
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("h01103")]
        public DateTime h01f03 { get; set; }

        /// <summary>
        /// Punch Type
        /// </summary>
        [JsonPropertyName("h01104")]
        public EnmPunchType? h01f04 { get; set; }

        /// <summary>
        /// Punch creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime h01f05 { get; set; }

        /// <summary>
        /// Punch last modified datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime h01f06 { get; set; }
    }
}
