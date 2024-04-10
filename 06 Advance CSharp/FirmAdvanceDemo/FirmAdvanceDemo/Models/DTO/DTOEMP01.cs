using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Employee DTO model
    /// </summary>
    public class DTOEMP01 : IDTOEMP01
    {
        /// <summary>
        /// Employee First Name
        /// </summary>
        [JsonPropertyName("p01f02")]
        public string p01102 { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary>
        [JsonPropertyName("p01f03")]
        public string p01103 { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        [JsonPropertyName("p01f04")]
        public char p01104 { get; set; }

        /// <summary>
        /// Employee Date of Birth
        /// </summary>
        [JsonPropertyName("p01f05")]
        public DateTime p01105 { get; set; }

        /// <summary>
        /// Position Id
        /// </summary>
        [JsonPropertyName("p01f06")]
        public int p01106 { get; set; }
    }
}
