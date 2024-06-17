using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO
{
    /// <summary>
    /// Login model
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Username
        /// </summary>
        /// <example>u1</example>
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Username must be atmost of 20 length.")]
        public string Username { get; set; }

        ///<summary>
        /// Password
        /// </summary>
        /// <example>123</example>
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(44, ErrorMessage = "Password must be atmost of 44 length.")]
        public string Password { get; set; }
    }
}
