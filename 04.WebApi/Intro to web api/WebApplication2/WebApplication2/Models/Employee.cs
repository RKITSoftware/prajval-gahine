using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Employee
    {
        /// <summary>
        /// Employee id
        /// </summary>
        public int p01f01 { get; set; }

        /// <summary>
        /// Employee name
        /// </summary>
        public string p01f02 { get; set; }

        public Employee(int id, string name)
        {
            this.p01f01 = id;
            this.p01f02 = name;
        }
    }
}