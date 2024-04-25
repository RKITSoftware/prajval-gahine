using FirmAdvanceDemo.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using FirmAdvanceDemo.Models.DTO.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Punch DTO model
    /// </summary>
    public class DTOPCH01
    {
        /// <summary>
        /// Punch id
        /// </summary>
        [JsonProperty("h01101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Punch ID cannot be empty.")]
        public int H01F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonProperty("h01102")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Employee ID cannot be empty.")]
        public int H01F02 { get; set; }

        /// <summary>
        /// Punch Type
        /// </summary>
        [JsonProperty("h01103")]
        public EnmPunchType H01F03 { get; set; }

        /// <summary>
        /// Punch datetime
        /// </summary>
        [JsonProperty("h01104")]
        [RequiredWhenAdmin(ErrorMessage = "Punch datetime field cannot be empty.")]
        public DateTime? H01F04 { get; set; }
    }
}
