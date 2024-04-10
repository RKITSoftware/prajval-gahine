using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Position POCO model
    /// </summary>
    public class PSN01 : IModel
    {
        /// <summary>
        /// Position Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("n01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Position Name
        /// </summary>
        [Unique]
        [ValidateNotNull]
        [JsonPropertyName("n01102")]
        public string n01f02 { get; set; }

        /// <summary>
        /// Annual Package (LPA)
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("n01103")]
        public double n01f03 { get; set; }

        /// <summary>
        /// Monthly Salary
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("n01104")]
        public double n01f04 { get; set; }

        /// <summary>
        /// Yearly Bonus
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("n01105")]
        public double n01f05 { get; set; }

        /// <summary>
        /// Department Id
        /// </summary>
        [ForeignKey(typeof(DPT01), OnDelete = "CASCADE")]
        [JsonPropertyName("n01106")]
        public int n01f06 { get; set; }

        /// <summary>
        /// Position creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime n01f07 { get; set; }

        /// <summary>
        /// Position last modified datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime n01f08 { get; set; }
    }
}
