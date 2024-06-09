using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOEXP01
    {
        /// <summary>
        /// Expense ID
        /// </summary>
        [JsonProperty("P01101")]
        public int P01F01 { get; set; }

        /// <summary>
        /// User ID (Payer)
        /// </summary>
        [JsonProperty("P01102")]
        [Required(ErrorMessage = "User ID is required")]
        public int P01F02 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("P01103")]
        public string P01F03 { get; set; }

        /// <summary>
        /// Amount paid
        /// </summary>
        [JsonProperty("P01104")]
        [Required(ErrorMessage = "Amount paid is required.")]
        public double P01F04 { get; set; }

        /// <summary>
        /// Date of Expense
        /// </summary>
        [JsonProperty("P01105")]
        public DateTime P01F05 { get; set; }
    }
}
