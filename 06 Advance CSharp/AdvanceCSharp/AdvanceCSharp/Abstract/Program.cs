namespace AdvanceCSharp.Abstract
{
    /// <summary>
    /// Program class for entry of abstract class demo
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main method - entry point for abstract class demo
        /// </summary>
        public static void Main()
        {
            Account sa = new SavingAccount();
            sa.Balance = 50000;
            sa.AccountDetails();

            Account ca = new CurrentAccount();
            ca.Balance = 20000;
            ca.AccountDetails();
        }
    }
}
