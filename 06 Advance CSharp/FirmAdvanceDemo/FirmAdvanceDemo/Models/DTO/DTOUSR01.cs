using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// User DTO model
    /// </summary>
    public class DTOUSR01
    {
        #region Public Properties
        /// <summary>
        /// user id
        /// </summary>
        [JsonProperty("r01101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "User ID cannot be empty.")]
        public int R01F01 { get; set; }

        /// <summary>
        /// username
        /// </summary>
        [JsonProperty("r01102")]
        [Required(ErrorMessage = "Username cannot be empty.")]
        public string R01F02 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        [JsonProperty("r01103")]
        [Required(ErrorMessage = "Password cannot be empty.")]
        public string R01F03 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        [JsonProperty("r01104")]
        [EmailAddress]
        [Required(ErrorMessage = "Email ID cannot be empty.")]
        public string R01F04 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        [JsonProperty("r01105")]
        [Phone]
        [Required(ErrorMessage = "Phone no. cannot be empty.")]
        public string R01F05 { get; set; }
        #endregion
    }
}
