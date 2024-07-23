using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOUSR01
    {
        /// <summary>
        /// User ID
        /// </summary>
        [JsonProperty("R01101")]
        [Required(ErrorMessage = "User Id is required")]
        [Range(0, int.MaxValue, ErrorMessage = "User Id cannot be less than 0")]
        public int R01F01 { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [JsonProperty("R01102")]
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Username must be atmost of 20 length.")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [JsonProperty("R01103")]
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(44, ErrorMessage = "Password must be atmost of 44 length.")]
        public string R01F03 { get; set; }
    }
}
