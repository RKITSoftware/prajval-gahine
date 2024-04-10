using FirmAdvanceDemo.Enums;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Leave POCO model
    /// </summary>
    [CompositeIndex(true, "e02f02", "e02f04")]
    public class LVE02 : IModel
    {
        /// <summary>
        /// Leave Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("e02f01")]
        public int Id { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        [JsonPropertyName("e02102")]
        public int e02f02 { get; set; }

        /// <summary>
        ///  Date of Request for Leave
        /// </summary>
        [ValidateNotNull]
        [TypeConverter("Date")]
        [JsonPropertyName("e02103")]
        public DateTime e02f03 { get; set; }

        /// <summary>
        /// Leave Date
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("e02104")]
        public DateTime e02f04 { get; set; }

        /// <summary>
        /// No. of leaves from Leave Date
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("e02105")]
        public int e02f05 { get; set; }

        /// <summary>
        /// Reason for leave
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("e02106")]
        public string e02f06 { get; set; }

        /// <summary>
        /// Leave status
        /// </summary>
        [ValidateNotNull]
        [DataType("int")]
        [JsonPropertyName("e02107")]
        public LeaveStatus e02f07 { get; set; }

        /// <summary>
        /// Leave creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime e02f08 { get; set; }

        /// <summary>
        /// Leave last modified datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime e02f09 { get; set; }
    }
}
