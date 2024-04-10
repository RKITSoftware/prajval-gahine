namespace AdvanceCSharp.Abstract
{
    /// <summary>
    /// Saving Account class
    /// </summary>
    public class SavingAccount : Account
    {
        /// <summary>
        /// Method to show account details
        /// </summary>
        public override void AccountDetails()
        {
            Console.WriteLine("Account type: Savings Account");
            Console.WriteLine("Account Balance: " + Balance);
        }
    }
}
