using System.Text.Json.Serialization;

namespace PocoDtoConversion.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int r01f01 { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        [JsonPropertyName("r01102")]
        public string r01f02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string r01f03 { get; set; }

        /// <summary>
        /// User Role
        /// </summary>
        [JsonPropertyName("r01104")]
        public string r01f04 { get; set; }
    }
}