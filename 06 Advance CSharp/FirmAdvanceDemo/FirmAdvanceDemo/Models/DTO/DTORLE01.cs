using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ServiceStack.LicenseUtils;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Role DTO model
    /// </summary>
    public class DTORLE01
    {
        /// <summary>
        /// Role id
        /// </summary>
        [JsonPropertyName("e01101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Role id cannot be empty.")]
        public int e01f01 { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [JsonPropertyName("e01102")]
        [Required(ErrorMessage = "Role name cannot be empty.")]
        public string e01f02 { get; set; }
    }
}
