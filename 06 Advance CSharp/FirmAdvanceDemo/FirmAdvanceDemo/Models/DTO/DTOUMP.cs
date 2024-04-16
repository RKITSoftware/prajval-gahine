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
        [JsonPropertyName("r01102")]
        public string r01f02 { get; set; }

        /// <summary>
        /// user password
        /// </summary>
        public string r01f03 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        [JsonPropertyName("r01104")]
        public string r01f04 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        [JsonPropertyName("r01105")]
        public string r01f05 { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        public List<EnmRole> r01f06 { get; set; }

        /// <summary>
        /// Employee First Name
        /// </summary>
        [JsonPropertyName("p01102")]
        public string p01f02 { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary>
        [JsonPropertyName("p01103")]
        public string p01f03 { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        [JsonPropertyName("p01104")]
        public char p01f04 { get; set; }

        /// <summary>
        /// Employee Date of Birth
        /// </summary>
        [JsonPropertyName("p01105")]
        public DateTime p01f05 { get; set; }

        /// <summary>
        /// Position Id
        /// </summary>
        [JsonPropertyName("p01106")]
        public int p01f06 { get; set; }
    }
}
