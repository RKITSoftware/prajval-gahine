using System;
using System.Collections;

namespace CSharpBasicApp
{
    internal class MyClassCompareCustomObject
    {
        class Customer : IComparable
        {
            public int Id;
            public string Name;
            public Customer(int Id, string Name)
            {
                this.Id = Id;
                this.Name = Name;
            }

            public int CompareTo(object obj)
            {
                Customer c = (Customer)obj;
                if(this.Id < c.Id)
                {
                    return -1;
                }
                else if(this.Id > c.Id)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        static void Main(string[] args)
        {
            ArrayList al = new ArrayList(10);

            al.Add(new Customer(101, "Cust1"));
            al.Add(new Customer(102, "Cust2"));
            al.Add(new Customer(103, "Cust3"));
            al.Add(new Customer(104, "Cust4"));
            al.Add(new Customer(105, "Cust5"));

            al.Sort();

            foreach (Customer c in al)
            {
                Console.WriteLine(c.Id + " " + c.Name);
            }
        }
    }
}
