using ServiceStack.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO
{
    public class DTOCNT01
    {
        /// <summary>
        /// User Expense Payee ID
        /// </summary>
        public int T01F01 { get; set; }

        /// <summary>
        /// Expense ID
        /// </summary>
        public int T01F02 { get; set; }

        /// <summary>
        /// User ID (Payee)
        /// </summary>
        public int T01F03 { get; set; }

        /// <summary>
        /// Amount to be paid
        /// </summary>
        public double T01F04 { get; set; }
    }
}
