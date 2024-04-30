using ServiceStack.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOEXP01
    {
        /// <summary>
        /// Expense ID
        /// </summary>
        public int P01F01 { get; set; }

        /// <summary>
        /// Payer User ID
        /// </summary>

        public int P01F02 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string P01F03 { get; set; }

        /// <summary>
        /// Amount paid
        /// </summary>
        public double P01F04 { get; set; }
    }
}
