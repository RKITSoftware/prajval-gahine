using FirmAdvanceDemo.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    public class DTOUMP : IDTOUSR01, IDTOEMP01
    {
        /// <summary>
        /// username
        /// </summary>
        [JsonPropertyName("r01f02")]
        public string r01102 { get; set; }

        /// <summary>
        /// user password
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
