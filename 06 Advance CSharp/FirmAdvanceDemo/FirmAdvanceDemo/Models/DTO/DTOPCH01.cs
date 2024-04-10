using FirmAdvanceDemo.Enums;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Punch DTO model
    /// </summary>
    public class DTOPCH01
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("h01f02")]
        public int h01102 { get; set; }

        /// <summary>
        /// DateTime of Punch
        /// </summary>
        [JsonPropertyName("h01f03")]
        public DateTime h01103 { get; set; }

        /// <summary>
        /// Punch Type
        /// </summary>
        [JsonPropertyName("h01f04")]
        public EnmPunchType? h01104 { get; set; }
    }
}
