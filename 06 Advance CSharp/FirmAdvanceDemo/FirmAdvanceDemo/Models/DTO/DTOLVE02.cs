using FirmAdvanceDemo.Enums;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Leave DTO model
    /// </summary>
    public class DTOLVE02
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("e02f02")]
        public int e02102 { get; set; }

        /// <summary>
        ///  Date of Request for Leave
        /// </summary>
        [JsonPropertyName("e02f03")]
        public DateTime e02103 { get; set; }

        /// <summary>
        /// Leave Date
        /// </summary>
        [JsonPropertyName("e02f04")]
        public DateTime e02104 { get; set; }

        /// <summary>
        /// No. of leaves from Leave Date
        /// </summary>
        [JsonPropertyName("e02f05")]
        public int e02105 { get; set; }

        /// <summary>
        /// Reason for leave
        /// </summary>
        [JsonPropertyName("e02f06")]
        public string e02106 { get; set; }

        /// <summary>
        /// Leave status
        /// </summary>
        [JsonPropertyName("e02f07")]
        public LeaveStatus e02107 { get; set; }
    }
}
