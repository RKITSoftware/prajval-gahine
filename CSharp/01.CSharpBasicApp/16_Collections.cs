using System;
using System.Collections;

namespace CSharpBasicApp
{

    internal class MyClassCollection
    {
        static void Main(string[] args)
        {
            // ArrayList is used when we have to access collection items using index
            ArrayList al = new ArrayList();
            Console.WriteLine("Capacity: " + al.Capacity);
            al.Add(1);
            Console.WriteLine("Capacity: " + al.Capacity);
            al.Add(3);
            al.Add(2);
            al.Add(5);
            al.Add(4);
            Console.WriteLine("Capacity: " + al.Capacity);

            al.Sort();
            al.Reverse();

            foreach(int i in al)
            {
                Console.WriteLine(i);
            }

            al.Insert(1, 100);
            al.RemoveAt(2);
            al.Remove(100);



            // HashTable is used when you want to access collection items using a key
            Hashtable ht = new Hashtable();
            ht.Add(101, "One");
            ht.Add(102, "two");
            ht.Add(103, "three");

            // accessing ht using key
            Console.WriteLine("Item at key 102 is: " + ht[102]);

            // iterating over ht using DictionaryEntry object
            foreach (DictionaryEntry de in ht)
            {
                Console.WriteLine(de.Key + " " + de.Value);
            }

            ht.Remove(102);
            Console.WriteLine(ht.ContainsKey(102));
            Console.WriteLine(ht.ContainsValue("two"));

            // iterating over ht using ht.Keys
            foreach(int key in ht.Keys)
            {
                Console.WriteLine(key + " " + ht[key]);
            }
        }
    }
}
