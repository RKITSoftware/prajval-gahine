using FirmAdvanceDemo.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ServiceStack.LicenseUtils;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Leave DTO model
    /// </summary>
    public class DTOLVE02
    {
        /// <summary>
        /// Leave id
        /// </summary>
        [JsonPropertyName("e02101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Leave id cannot be empty.")]
        public int e02f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("e02102")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Employee id cannot be empty.")]
        public int e02f02 { get; set; }

        /// <summary>
        ///  Date of Request for Leave
        /// </summary>
        [JsonPropertyName("e02103")]
        [Required(ErrorMessage = "Date of request cannot be empty.")]
        public DateTime e02f03 { get; set; }

        /// <summary>
        /// Leave Date
        /// </summary>
        [JsonPropertyName("e02104")]
        [Required(ErrorMessage = "Leave date must be selected.")]
        public DateTime e02f04 { get; set; }

        /// <summary>
        /// No. of leaves from Leave Date
        /// </summary>
        [JsonPropertyName("e02105")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "No. of leaves cannot be empty.")]
        public int e02f05 { get; set; }

        /// <summary>
        /// Reason for leave
        /// </summary>
        [JsonPropertyName("e02106")]
        [Required(ErrorMessage = "Reason of leave cannot be empty.")]
        public string e02f06 { get; set; }

        /// <summary>
        /// Leave status
        /// </summary>
        [JsonPropertyName("e02107")]
        public LeaveStatus e02f07 { get; set; }
    }
}
