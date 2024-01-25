using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo.Model
{
    internal class Employee
    {
        public int id { get; set; }

        public string name { get; set; }

        public DateOnly date_of_joining { get; set; }

        public Employee(string name, DateOnly doj) : this(-1, name, doj)
        {
            this.id = -1;
            this.name = name;
            this.date_of_joining = doj;
        }

        public Employee(int id, string name, DateOnly date_of_joining)
        {
            this.id = id;
            this.name = name;
            this.date_of_joining = date_of_joining;
        }
    }
}
