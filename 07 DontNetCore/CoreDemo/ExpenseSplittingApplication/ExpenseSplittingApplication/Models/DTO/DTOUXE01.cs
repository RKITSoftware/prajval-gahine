using ServiceStack.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOUXE01
    {
        /// <summary>
        /// User Expense Payee ID
        /// </summary>
        public int E01F01 { get; set; }

        /// <summary>
        /// Expense ID
        /// </summary>
        public int E01F02 { get; set; }

        /// <summary>
        /// User ID (Payee)
        /// </summary>
        public int E01F03 { get; set; }

        /// <summary>
        /// Amount to be paid
        /// </summary>
        public double E01F04 { get; set; }
    }
}
