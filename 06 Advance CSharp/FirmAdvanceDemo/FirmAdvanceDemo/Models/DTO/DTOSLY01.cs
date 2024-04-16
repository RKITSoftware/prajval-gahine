using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ServiceStack.LicenseUtils;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Salary DTO model
    /// </summary>
    public class DTOSLY01
    {
        /// <summary>
        /// Salary id
        /// </summary>
        [JsonPropertyName("y01101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Salary id cannot be empty.")]
        public int y01f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("y01102")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Employee id cannot be empty.")]
        public int y01f02 { get; set; }

        /// <summary>
        /// Month, Year of salary credit
        /// </summary>
        [JsonPropertyName("y01103")]
        [Required(ErrorMessage = "Salary credit date cannot be empty.")]
        public DateTime y01f03 { get; set; }

        /// <summary>
        /// Monthly salary amount
        /// </summary>
        [JsonPropertyName("y01104")]
        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "Monthly salary amount cannot be empty.")]
        public double y01f04 { get; set; }

        /// <summary>
        /// Position Id
        /// </summary>
        [JsonPropertyName("y01105")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Position id cannot be empty.")]
        public int y01f05 { get; set; }
    }
}
