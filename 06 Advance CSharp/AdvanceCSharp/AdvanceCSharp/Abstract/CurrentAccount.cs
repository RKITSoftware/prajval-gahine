namespace AdvanceCSharp.Abstract
{
    /// <summary>
    /// Current Account class
    /// </summary>
    public class CurrentAccount : Account
    {
        /// <summary>
        /// Method to show account details
        /// </summary>
        public override void AccountDetails()
        {
            Console.WriteLine("Account type: Current Account");
            Console.WriteLine("Account Balance: " + Balance);
        }
    }
}
