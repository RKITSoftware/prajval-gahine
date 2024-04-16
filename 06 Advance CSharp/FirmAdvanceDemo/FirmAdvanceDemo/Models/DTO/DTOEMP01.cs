using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ServiceStack.LicenseUtils;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Employee DTO model
    /// </summary>
    public class DTOEMP01 : IDTOEMP01
    {
        /// <summary>
        /// Employee id
        /// </summary>
        [JsonPropertyName("p01101")]
        [Required(ErrorMessage = "Employee id cannot be empty.")]
        [Range(0, int.MaxValue)]
        public int p01f01 { get; set; }

        /// <summary>
        /// Employee First Name
        /// </summary>
        [JsonPropertyName("p01102")]
        [Required(ErrorMessage = "First name cannot be empty.")]
        public string p01f02 { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary>
        [JsonPropertyName("p01103")]
        [Required(ErrorMessage = "Last name cannot be empty.")]
        public string p01f03 { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        [JsonPropertyName("p01104")]
        [Required(ErrorMessage = "Gender must be selected.")]
        public char p01f04 { get; set; }

        /// <summary>
        /// Employee Date of Birth
        /// </summary>
        [JsonPropertyName("p01105")]
        [Required(ErrorMessage = "Date of Birth must be selected.")]
        public DateTime p01f05 { get; set; }

        /// <summary>
        /// Position Id
        /// </summary>
        [JsonPropertyName("p01106")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Position id cannot be empty.")]
        public int p01f06 { get; set; }
    }
}
