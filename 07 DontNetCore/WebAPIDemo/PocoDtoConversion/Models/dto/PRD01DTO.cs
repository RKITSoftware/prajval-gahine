using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PocoDtoConversion.Models.dto
{
    /// <summary>
    /// Product dto
    /// </summary>
    public class PRD01DTO
    {
        /// <summary>
        /// Product Name
        /// </summary>
        public string d01102 { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        public float d01103 { get; set; }
    }
}
