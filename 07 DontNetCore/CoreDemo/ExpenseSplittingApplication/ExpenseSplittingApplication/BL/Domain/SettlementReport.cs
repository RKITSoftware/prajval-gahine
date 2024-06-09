using System.Collections.Generic;

namespace ExpenseSplittingApplication.BL.Domain
{
    public class SettlementReport
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public Dictionary<int, double> Recievables { get; set; }

        public Dictionary<int, double> Payables { get; set; }
    }
}
