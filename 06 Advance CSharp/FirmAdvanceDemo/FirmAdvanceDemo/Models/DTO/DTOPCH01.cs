using FirmAdvanceDemo.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ServiceStack.LicenseUtils;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Punch DTO model
    /// </summary>
    public class DTOPCH01
    {

        /// <summary>
        /// Punch id
        /// </summary>
        [JsonPropertyName("h01101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Punch id cannot be empty.")]
        public int h01f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("h01102")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Employee id cannot be empty.")]
        public int h01f02 { get; set; }

        /// <summary>
        /// DateTime of Punch
        /// </summary>
        [JsonPropertyName("h01103")]
        [Required(ErrorMessage = "Date of punch cannot be empty.")]
        public DateTime h01f03 { get; set; }

        /// <summary>
        /// Punch Type
        /// </summary>
        [JsonPropertyName("h01104")]
        public EnmPunchType? h01f04 { get; set; }
    }
}
