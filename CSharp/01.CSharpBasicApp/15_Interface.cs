using System;
using System.Data;
using System.Net.Http.Headers;
using MyInterfaceDemo;

namespace MyInterfaceDemo
{
    // bydefaul all members(methods, properties, events) are public and abstract.
    interface IPerson
    {
        string Name
        { get; set; }
        public abstract void Describe();
    }

    class Student : IPerson
    {
        public string Name { get; set; }

        public Student()
        {}
        public Student(string Name)
        {
            this.Name = Name;
        }
        public void Describe()
        {
            Console.WriteLine("Hi i am a student and my name is, " + Name);
        }
    }

    class Faculty : IPerson
    {
        public string Name
        { get; set; }
        public Faculty() { }

        public Faculty(string Name)
        {
            this.Name = Name; 
        }

        public void Describe()
        {
            Console.WriteLine("Hi i am a faculty and my name is, " + Name);
        }
    }
}

namespace CSharpBasicApp
{
    internal class MyClassInterface
    {
        static void Main()
        {
            MyInterfaceDemo.IPerson p;
            string role;
            role = Console.ReadLine();
            if (role == "Student")
            {
                p = new MyInterfaceDemo.Student("Prajval");
            }
            else if(role == "Faculty")
            {
                p = new MyInterfaceDemo.Faculty("Arya");
            }
            else
            {
                throw new ApplicationException("Provide valid role");
            }
            p.Describe();
        }
    }
}
