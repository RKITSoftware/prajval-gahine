using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Attendance DTO model
    /// </summary>
    public class DTOATD01
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("d01f02")]
        public int d01102 { get; set; }

        /// <summary>
        /// Date of attendance
        /// </summary>
        [JsonPropertyName("d01f03")]
        public DateTime d01103 { get; set; }

        /// <summary>
        /// Day work hour
        /// </summary>
        [JsonPropertyName("d01f04")]
        public double d01104 { get; set; }
    }
}
