using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Department DTO model
    /// </summary>
    public class DTODPT01
    {
        /// <summary>
        /// Department Id
        /// </summary>
        [JsonProperty("t01101")]
        [Required(ErrorMessage = "Department ID cannot be empty.")]
        [Range(0, int.MaxValue)]
        public int T01F01 { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [JsonProperty("t01102")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Department name cannot be empty.")]
        public string T01F02 { get; set; }
    }
}
