using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [Required(ErrorMessage = "Salary ID cannot be empty.")]
        public int Y01F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("y01102")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Employee ID cannot be empty.")]
        public int Y01F02 { get; set; }

        /// <summary>
        /// Month, Year of salary credit
        /// </summary>
        [JsonPropertyName("y01103")]
        [Required(ErrorMessage = "Salary credit date cannot be empty.")]
        public DateTime Y01F03 { get; set; }

        /// <summary>
        /// Monthly salary amount
        /// </summary>
        [JsonPropertyName("y01104")]
        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "Monthly salary amount cannot be empty.")]
        public double Y01F04 { get; set; }

        /// <summary>
        /// Position Id
        /// </summary>
        [JsonPropertyName("y01105")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Position ID cannot be empty.")]
        public int Y01F05 { get; set; }
    }
}
