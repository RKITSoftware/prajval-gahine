using FirmAdvanceDemo.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// User DTO model
    /// </summary>
    public class DTOUSR01 : IDTOUSR01
    {
        /// <summary>
        /// username
        /// </summary>
        [JsonPropertyName("r01f02")]
        public string r01102 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        public string r01103 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        [JsonPropertyName("r01f04")]
        public string r01104 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        [JsonPropertyName("r01f05")]
        public string r01105 { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        public List<EnmRole> r01106 { get; set; }
    }
}
