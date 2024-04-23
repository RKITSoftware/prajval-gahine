using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Position DTO model
    /// </summary>
    public class DTOPSN01
    {

        /// <summary>
        /// Position id
        /// </summary>
        [JsonProperty("n01101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Position ID cannot be empty.")]
        public int N01F01 { get; set; }

        /// <summary>
        /// Position name
        /// </summary>
        [JsonProperty("n01102")]
        [Required(ErrorMessage = "Position name cannot be empty.")]
        public string N01F02 { get; set; }

        /// <summary>
        /// Annual Package (LPA)
        /// </summary>
        [JsonProperty("n01103")]
        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "Annual Package cannot be empty.")]
        public double N01F03 { get; set; }

        /// <summary>
        /// Monthly Salary
        /// </summary>
        [JsonProperty("n01104")]
        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "Monthly salary cannot be empty.")]
        public double N01F05 { get; set; }

        /// <summary>
        /// Yearly Bonus
        /// </summary>
        [JsonProperty("n01105")]
        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "Yearly bonus cannot be empty.")]
        public double N01F04 { get; set; }

        /// <summary>
        /// Department Id
        /// </summary>
        [JsonProperty("n01106")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Department ID cannot be empty.")]
        public int N01F06 { get; set; }
    }
}
