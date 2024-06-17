using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOCNT01
    {
        /// <summary>
        /// User Expense Payee ID
        /// </summary>
        [JsonProperty("T01101")]
        [Required(ErrorMessage = "Contributor Id is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Contributor Id cannot be less than 0")]
        public int T01F01 { get; set; }

        /// <summary>
        /// Expense ID
        /// </summary>
        [JsonProperty("T01102")]
        [Required(ErrorMessage = "Expense Id is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Expense Id cannot be less than 0")]
        public int T01F02 { get; set; }

        /// <summary>
        /// User ID (Payee)
        /// </summary>
        [JsonProperty("T01103")]
        [Required(ErrorMessage = "Payee User ID is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "User Id cannot be less than 0")]
        public int T01F03 { get; set; }

        /// <summary>
        /// Amount to be paid
        /// </summary>
        [JsonProperty("T01104")]
        [Required(ErrorMessage = "Amount to be paid is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Amount cannot be less than 0")]
        public double T01F04 { get; set; }
    }
}
