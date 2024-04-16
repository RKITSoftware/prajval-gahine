using FirmAdvanceDemo.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.DTO
{
    /// <summary>
    /// User DTO model
    /// </summary>
    public class DTOUSR01 : IDTOUSR01
    {
        /// <summary>
        /// user id
        /// </summary>
        [JsonPropertyName("r01101")]
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "User id cannot be empty.")]
        public int r01f01 {  get; set; }

        /// <summary>
        /// username
        /// </summary>
        [JsonPropertyName("r01102")]
        [Required(ErrorMessage = "Username cannot be empty.")]
        public string r01f02 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        [Required(ErrorMessage = "Password cannot be empty.")]
        public string r01f03 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        [JsonPropertyName("r01104")]
        [EmailAddress]
        [Required(ErrorMessage = "Email id cannot be empty.")]
        public string r01f04 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        [JsonPropertyName("r01105")]
        [Phone]
        [Required(ErrorMessage = "Phone no. cannot be empty.")]
        public string r01f05 { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        [Required(ErrorMessage = "Roles cannot be empty.")]
        public List<EnmRole> r01f06 { get; set; }
    }
}
