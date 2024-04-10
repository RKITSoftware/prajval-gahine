using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Attendance POCO model
    /// </summary>
    [CompositeIndex(unique: true, "d01f02", "d01f03")]
    public class ATD01 : IModel
    {
        /// <summary>
        /// Attendance Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("d01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        [JsonPropertyName("d01102")]
        public int d01f02 { get; set; }

        /// <summary>
        /// Date of attendance
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("d01103")]
        public DateTime d01f03 { get; set; }

        /// <summary>
        /// Day work hour
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("d01104")]
        public double d01f04 { get; set; }

        /// <summary>
        /// Attendance creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime d01f05 { get; set; }

        /// <summary>
        /// Attendance last modified datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime d01f06 { get; set; }
    }
}
