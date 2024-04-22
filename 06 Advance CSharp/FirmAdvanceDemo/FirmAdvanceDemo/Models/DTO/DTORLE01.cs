using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Role DTO model
    /// </summary>
    public class DTORLE01
    {
        /// <summary>
        /// Role id
        /// </summary>
        [JsonProperty("e01101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Role ID cannot be empty.")]
        public int E01F01 { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [JsonProperty("e01102")]
        [Required(ErrorMessage = "Role name cannot be empty.")]
        public string E01F02 { get; set; }
    }
}
