using System.Collections.Generic;

namespace ExpenseSplittingApplication.BL.Domain
{
    /// <summary>
    /// Represents a settlement report containing financial details for a user.
    /// </summary>
    public class SettlementReport
    {
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of receivables where the key is the user ID and the value is the amount.
        /// </summary>
        public Dictionary<int, double> Receivables { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of payables where the key is the user ID and the value is the amount.
        /// </summary>
        public Dictionary<int, double> Payables { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of final dues where the key is the user ID and the value is the amount.
        /// </summary>
        public Dictionary<int, double> FinalDues { get; set; }
    }
}
