using FirmAdvanceDemo.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("h01101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Punch ID cannot be empty.")]
        public int H01F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [JsonPropertyName("h01102")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Employee ID cannot be empty.")]
        public int H01F02 { get; set; }

        /// <summary>
        /// Punch Type
        /// </summary>
        [JsonPropertyName("h01104")]
        public EnmPunchType H01F04 { get; set; }
    }
}
