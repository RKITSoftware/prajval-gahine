namespace AdvanceCSharp.Abstract
{
    /// <summary>
    /// Account class (abstract)
    /// </summary>
    public abstract class Account
    {

        /// <summary>
        /// Max transaction limit
        /// </summary>
        public static float maxTransactionLimit = 25000;

        /// <summary>
        /// Account Balance
        /// </summary>
        private float _Balance;

        /// <summary>
        /// Account Balance getter and setter
        /// </summary>
        public float Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                if (Math.Abs(value) > maxTransactionLimit)
                {
                    throw new Exception("maximum transaction limit exceeded");
                }
                else
                {
                    _Balance = _Balance + value;
                }
            }
        }

        /// <summary>
        /// Method to show account details
        /// </summary>
        public abstract void AccountDetails();

    }
}
