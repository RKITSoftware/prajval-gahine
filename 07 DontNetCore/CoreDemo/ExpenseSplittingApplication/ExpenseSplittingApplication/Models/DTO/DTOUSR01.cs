using ExpenseSplittingApplication.Models.DTO.Filters;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOUSR01
    {
        /// <summary>
        /// User ID
        /// </summary>
        /// <example>0</example>
        public int R01F01 { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [RequiredForPost(ErrorMessage = "Password is required")]
        public string R01F03 { get; set; }
    }
}
