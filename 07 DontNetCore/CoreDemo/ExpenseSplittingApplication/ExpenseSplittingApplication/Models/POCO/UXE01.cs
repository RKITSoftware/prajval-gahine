using ServiceStack.DataAnnotations;

namespace ExpenseSplittingApplication.Models.POCO
{
    public class UXE01
    {
        /// <summary>
        /// User Expense Payee ID
        /// </summary>
        [PrimaryKey]
        public int E01F01 { get; set; }

        /// <summary>
        /// Expense ID
        /// </summary>
        public int E01F02 { get ; set; }

        /// <summary>
        /// User ID (Payee)
        /// </summary>
        public int E01F03 { get; set; }

        /// <summary>
        /// Amount to be paid
        /// </summary>
        public double E01F04 { get; set; }

        /// <summary>
        /// Is paid
        /// </summary>
        public bool E01F05 { get; set; }

        /// <summary>
        /// User Expense Payee creation datetime
        /// </summary>
        public DateTime E01F98 { get; set; }

        /// <summary>
        /// User Expense Payee last modified datetime
        /// </summary>
        public DateTime? E01F99 { get; set; }
    }
}
