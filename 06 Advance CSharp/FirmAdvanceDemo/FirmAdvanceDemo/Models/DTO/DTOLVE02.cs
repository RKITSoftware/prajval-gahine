using FirmAdvanceDemo.Enums;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

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
        [JsonProperty("e02101")]
        [Range(0, int.MaxValue, ErrorMessage = "Leave ID cannot be less than 0.")]
        [Required(ErrorMessage = "Leave ID cannot be empty.")]
        public int E02F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonProperty("e02102")]
        [Range(0, int.MaxValue, ErrorMessage = "Employee ID cannot be less than 0.")]
        [Required(ErrorMessage = "Employee ID cannot be empty.")]
        public int E02F02 { get; set; }

        /// <summary>
        /// Leave Date
        /// </summary>
        [JsonProperty("e02104")]
        [Required(ErrorMessage = "Leave date must be selected.")]
        public DateTime E02F03 { get; set; }

        /// <summary>
        /// No. of leaves from Leave Date
        /// </summary>
        [JsonProperty("e02105")]
        [Range(1, 60, ErrorMessage = "No. of leave must be in range 1 to 60.")]
        [Required(ErrorMessage = "No. of leaves cannot be empty.")]
        public int E02F04 { get; set; }

        /// <summary>
        /// Reason for leave
        /// </summary>
        [JsonProperty("e02106")]
        [Required(ErrorMessage = "Reason of leave cannot be empty.")]
        public string E02F05 { get; set; }

        /// <summary>
        /// Leave status
        /// </summary>
        [JsonProperty("e02107")]
        public EnmLeaveStatus E02F06 { get; set; }
    }
}
