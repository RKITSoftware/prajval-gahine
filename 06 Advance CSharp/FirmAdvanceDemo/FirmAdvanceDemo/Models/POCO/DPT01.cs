using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Department POCO model
    /// </summary>
    public class DPT01 : IModel
    {
        /// <summary>
        /// Department Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("t01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("t01102")]
        public string t01f02 { get; set; }

        /// <summary>
        /// Department creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime t01f03 { get; set; }

        /// <summary>
        /// Department last modified datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime t01f04 { get; set; }
    }
}
