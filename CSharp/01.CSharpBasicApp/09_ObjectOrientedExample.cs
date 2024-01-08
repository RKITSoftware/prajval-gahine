
using System;

namespace CSharpBasicApp
{
    /// <summary>
    /// 
    /// </summary>
    internal class Account : IDisposable
    {
        int AccountNo;
        public string Name;
        decimal Balance;
        public Account()
        {
            this.Name = "png";
            Console.WriteLine("account object created");
        }
        //~Account()
        //{
        //    Console.WriteLine("account object destroyed");
        //}
        public void Dispose()
        {
            // unmanaged code clean up
            Console.WriteLine("account object ready for destroy, from dispose");
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <param name="Name"></param>
        /// <param name="Balance"></param>
        void FillAccountDetails(int AccountNo, string Name, decimal Balance)
        {
            this.AccountNo = AccountNo;
            this.Name = Name;
            this.Balance = Balance;
        }
    }
    internal class ObjectOrientedExample
    {
        static void Foo()
        {
            Console.ReadLine();
            Account b = new Account();
            b.Dispose();
            Console.WriteLine(b.Name);
            //GC.Collect();
        }
        static void Main()
        {
            Foo();
        }
    }
}

/*
// When below code is executed using .NET framework => the destructor get called when the scope of a ends.
// Which is not the way how .NET Core has implemented GC in it's architecture.
using System;

namespace CSharpBasicApp
{
    class ObjectOrientedExample
    {
        static void Foo()
        {
            Account a = new Account();
        }
        static void Main()
        {
            Foo();
        }
    }

    class Account
    {
        public Account()
        {
            Console.WriteLine("object created");
        }
        ~Account()
        {
            Console.WriteLine("object destroyed");
        }
    }
}
*/