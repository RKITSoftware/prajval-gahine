using FirmAdvanceDemo.Models.DTO.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Attendance DTO model
    /// </summary>
    public class DTOATD01
    {
        /// <summary>
        /// attendance id
        /// </summary>
        [JsonPropertyName("d01101")]
        [Required(ErrorMessage = "Attendance ID cannot be empty.")]
        [Range(0, int.MaxValue)]
        public int D01F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("d01102")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Employee ID cannot be empty.")]
        public int D01F02 { get; set; }

        /// <summary>
        /// Date of attendance
        /// </summary>
        [JsonPropertyName("d01103")]
        [Required(ErrorMessage = "Attendance date cannot be empty")]
        [LessThanCurrentDateTime(ErrorMessage = "Attendance date cannot be greater than today's date.")]
        public DateTime D01F03 { get; set; }

        /// <summary>
        /// Day work hour
        /// </summary>
        [JsonPropertyName("d01104")]
        public double D01F04 { get; set; }
    }
}
