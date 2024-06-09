using ExpenseSplittingApplication.Models.DTO.Filters;
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
        [NonZeroUnsingedForPut(ErrorMessage = "userid cannot be 0 for updation")]
        [ZeroForPost(ErrorMessage = "Userid must be 0 for user creation")]
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
        [RequiredForPost(ErrorMessage = "Password is required")]
        [MaxLength(44, ErrorMessage = "Username must be atmost of 44 length.")]
        public string R01F03 { get; set; }
    }
}
