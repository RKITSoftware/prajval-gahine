using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestWebApp.Controllers
{
    class MyClass
    {
        public static List<int> myList;
        static MyClass()
        {
            myList = new List<int> { 1, 2, 3 };
        }

        public static List<int> AddInt(int num)
        {
            myList.Add(num);
            return myList;
        }
    }
    public class DefaultController : ApiController
    {
        [HttpGet]
        public List<int> GetInt()
        {
            return MyClass.myList;
        }

        [HttpPost]
        public List<int> PostInt(int num)
        {
            return MyClass.AddInt(num);
        }
    }
}
