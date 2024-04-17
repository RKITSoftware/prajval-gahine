using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTO
{
    /// <summary>
    /// Represents a DTO for a user entity in the system.
    /// </summary>
    public class DTOUSR01
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [JsonProperty("r01101")]
        [Required(ErrorMessage = "Id field cannot be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be greater than or equal to 0.")]
        public int R01F01 { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [JsonProperty("r01102")]
        [Required(ErrorMessage = "Username field cannot be empty")]
        [Range(1, 10)]
        public string R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the password of user.
        /// </summary>
        [JsonProperty("r01103")]
        [Range(1, 20)]
        [Required(ErrorMessage = "Password field cannot be empty")]
        public string R01F03 { get; set; }

        #endregion
    }
}