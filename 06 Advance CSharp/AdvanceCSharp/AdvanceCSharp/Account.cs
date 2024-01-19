using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceCSharp
{
    internal abstract class Account
    {
        static public float maxTransactionLimit = 25000;
        private float _Balance;
        public float Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                if(Math.Abs(value) > maxTransactionLimit)
                {
                    throw new Exception("maximum transaction limit exceeded");
                }
                else
                {
                    _Balance = _Balance + value;
                }
            }
        }
        public float CheckBalance()
        {
            return Balance;
        }
    }


    class SavingAccount : Account
    {
        public float ShowmaxTransactionLimit()
        {

            Type t1 = this.GetType();
            return SavingAccount.maxTransactionLimit;
        }
    }
}
