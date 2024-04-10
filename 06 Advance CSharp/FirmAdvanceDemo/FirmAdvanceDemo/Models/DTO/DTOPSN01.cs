using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    public class DTOPSN01
    {
        /// <summary>
        /// Position DTO model
        /// </summary>
        [JsonPropertyName("n01f02")]
        public string n01102 { get; set; }

        /// <summary>
        /// Annual Package (LPA)
        /// </summary>
        [JsonPropertyName("n01f03")]
        public double n01103 { get; set; }

        /// <summary>
        /// Monthly Salary
        /// </summary>
        [JsonPropertyName("n01f04")]
        public double n01104 { get; set; }

        /// <summary>
        /// Yearly Bonus
        /// </summary>
        [JsonPropertyName("n01f05")]
        public double n01105 { get; set; }

        /// <summary>
        /// Department Id
        /// </summary>
        [JsonPropertyName("n01f06")]
        public int n01106 { get; set; }
    }
}
