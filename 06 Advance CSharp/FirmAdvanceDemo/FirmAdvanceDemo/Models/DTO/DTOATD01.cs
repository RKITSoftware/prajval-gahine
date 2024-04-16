using FirmAdvanceDemo.Models.DTO.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ServiceStack.LicenseUtils;

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
        [Required(ErrorMessage = "Attendance id cannot be empty.")]
        [Range(0, int.MaxValue)]
        public int d01f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("d01102")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Employee id cannot be empty.")]
        public int d01f02 { get; set; }

        /// <summary>
        /// Date of attendance
        /// </summary>
        [JsonPropertyName("d01103")]
        [Required(ErrorMessage = "Attendance date cannot be empty")]
        [LessThanCurrentDate(ErrorMessage = "Attendance date cannot be greater than today's date.")]
        public DateTime d01f03 { get; set; }

        /// <summary>
        /// Day work hour
        /// </summary>
        [JsonPropertyName("d01104")]
        public double d01f04 { get; set; }
    }
}
