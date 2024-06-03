using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOEXP01
    {
        /// <summary>
        /// Expense ID
        /// </summary>
        public int P01F01 { get; set; }

        /// <summary>
        /// User ID (Payer)
        /// </summary>
        [Required(ErrorMessage = "User ID is required")]
        public int P01F02 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string P01F03 { get; set; }

        /// <summary>
        /// Amount paid
        /// </summary>
        [Required(ErrorMessage = "Amount paid is required.")]
        public double P01F04 { get; set; }

        /// <summary>
        /// Date of Expense
        /// </summary>
        public DateTime P01F05 { get; set; }
    }
}
