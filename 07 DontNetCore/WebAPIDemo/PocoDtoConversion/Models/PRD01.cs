
using System.Text.Json.Serialization;

namespace PocoDtoConversion.Models
{
    /// <summary>
    /// Product model
    /// </summary>
    public class PRD01
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int d01f01 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [JsonPropertyName("d01102")]
        public string d01f02 { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        [JsonPropertyName("d01103")]
        public float d01f03 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        public int d01f04 { get; set; }

        /// <summary>
        /// Supplier id
        /// </summary>
        public int d01f05 { get; set; }
    }
}

