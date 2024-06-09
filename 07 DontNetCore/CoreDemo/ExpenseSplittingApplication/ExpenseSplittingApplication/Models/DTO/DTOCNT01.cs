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
        public int T01F01 { get; set; }

        /// <summary>
        /// Expense ID
        /// </summary>
        [JsonProperty("T01102")]
        public int T01F02 { get; set; }

        /// <summary>
        /// User ID (Payee)
        /// </summary>
        [JsonProperty("T01103")]
        [Required(ErrorMessage = "Payee User ID is required.")]
        public int T01F03 { get; set; }

        /// <summary>
        /// Amount to be paid
        /// </summary>
        [JsonProperty("T01104")]
        [Required(ErrorMessage = "Amount to be paid is required.")]
        public double T01F04 { get; set; }
    }
}
