using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Department DTO model
    /// </summary>
    public class DTODPT01
    {
        /// <summary>
        /// Department Id
        /// </summary>
        [JsonPropertyName("t01101")]
        [Required(ErrorMessage = "Department id cannot be empty.")]
        [Range(0, int.MaxValue)]
        public int t01f01 { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [JsonPropertyName("t01102")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Department name cannot be empty.")]
        public string t01f02 { get; set; }
    }
}
