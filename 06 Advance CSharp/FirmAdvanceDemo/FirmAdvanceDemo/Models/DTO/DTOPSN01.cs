using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ServiceStack.LicenseUtils;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Position DTO model
    /// </summary>
    public class DTOPSN01
    {

        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("n01101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Position id cannot be empty.")]
        public string n01f01 { get; set; }

        /// <summary>
        /// Position name
        /// </summary>
        [JsonPropertyName("n01102")]
        [Required(ErrorMessage = "Position name cannot be empty.")]
        public string n01f02 { get; set; }

        /// <summary>
        /// Annual Package (LPA)
        /// </summary>
        [JsonPropertyName("n01103")]
        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "Annual Package cannot be empty.")]
        public double n01f03 { get; set; }

        /// <summary>
        /// Monthly Salary
        /// </summary>
        [JsonPropertyName("n01104")]
        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "Monthly salary cannot be empty.")]
        public double n01f05 { get; set; }

        /// <summary>
        /// Yearly Bonus
        /// </summary>
        [JsonPropertyName("n01105")]
        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "Yearly bonus cannot be empty.")]
        public double n01f04 { get; set; }

        /// <summary>
        /// Department Id
        /// </summary>
        [JsonPropertyName("n01106")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Department id cannot be empty.")]
        public int n01f06 { get; set; }
    }
}
