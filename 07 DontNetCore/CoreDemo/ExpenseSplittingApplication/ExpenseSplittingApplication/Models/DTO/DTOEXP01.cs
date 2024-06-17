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
        [Required(ErrorMessage = "Expense Id is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Expense Id cannot be less than 0")]
        public int P01F01 { get; set; }

        /// <summary>
        /// User ID (Payer)
        /// </summary>
        [JsonProperty("P01102")]
        [Required(ErrorMessage = "User Id is required")]
        [Range(0, int.MaxValue, ErrorMessage = "User Id cannot be less than 0")]
        public int P01F02 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("P01103")]
        //[MaxLength(150)]
        public string P01F03 { get; set; }

        /// <summary>
        /// Amount paid
        /// </summary>
        [JsonProperty("P01104")]
        [Required(ErrorMessage = "Amount paid is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Amount cannot be less than 0")]
        public double P01F04 { get; set; }

        /// <summary>
        /// Date of Expense
        /// </summary>
        [JsonProperty("P01105")]
        public DateTime? P01F05 { get; set; }
    }
}
