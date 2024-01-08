using System;

namespace CSharpBasicApp
{
    class Account2
    {
        private int _Id;
        private string _Name;
        private decimal _Balance;


        public string ContactNo
        {
            get; set;
        }


        private bool _isIdAlreadySet;
        // perfomring some "logical task / buisness logic" at time of setting a property's field member
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if(_isIdAlreadySet)
                {
                    throw new ApplicationException("Id is already set");
                }
                _isIdAlreadySet = true;
                _Id = value;
            }
        }

        // "validating" an field member before consuming it in set block
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if(value.Length > 0 && value.Length <= 8)
                {
                    _Name = value;
                }
                else
                {
                    throw new ApplicationException("Name must be of atleast one and atmost eight characters");
                }
            }
        }

        // ready-only property - "access restriction"
        public decimal Balance
        {
            get
            {
                //Console.WriteLine("HELLO");
                //Console.WriteLine(this);
                return _Balance;
            }
            private set
            {
                // some code here
                _Balance = value;
            }
        }

        // property can also be used in case of if you want private field but some code should be executed while setting that field.

        public void Deposit(decimal amount)
        {
            _Balance += amount;
        }
    }
    internal class Encapsulation
    {
        static void Main()
        {
            Account2 a = new Account2();
            //a.Balance = 1000; // cannot get since now Balance is ready only property
            //Console.WriteLine(a.Balance);
            a.ContactNo = "9924380554";
            Console.WriteLine(a.ContactNo);
            a.Deposit(1000);
        }
    }
}
