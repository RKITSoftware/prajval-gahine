using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp.BL
{
    class BLMyClass
    {
        public static List<int> myList;
        static BLMyClass()
        {
            myList = new List<int> { 1, 2, 3 };
        }

        public static List<int> AddInt(int num)
        {
            myList.Add(num);
            return myList;
        }
    }
}