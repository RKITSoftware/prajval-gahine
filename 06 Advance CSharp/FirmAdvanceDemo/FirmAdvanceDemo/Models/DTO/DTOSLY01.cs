using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Salary DTO model
    /// </summary>
    public class DTOSLY01
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("y01f02")]
        public int y01102 { get; set; }

        /// <summary>
        /// Month, Year of salary credit
        /// </summary>
        [JsonPropertyName("y01f03")]
        public DateTime y01103 { get; set; }

        /// <summary>
        /// Monthly salary amount
        /// </summary>
        [JsonPropertyName("y01f04")]
        public double y01104 { get; set; }

        /// <summary>
        /// Position Id
        /// </summary>
        [JsonPropertyName("y01f05")]
        public int y01105 { get; set; }
    }
}
