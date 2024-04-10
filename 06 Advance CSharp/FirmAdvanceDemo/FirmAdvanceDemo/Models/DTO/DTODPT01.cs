using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// Department DTO model
    /// </summary>
    public class DTODPT01
    {
        /// <summary>
        /// Department Name
        /// </summary>
        [JsonPropertyName("t01f02")]
        public string t01102 { get; set; }
    }
}
