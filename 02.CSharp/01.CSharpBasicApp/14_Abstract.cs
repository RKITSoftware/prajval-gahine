using System;

namespace CSharpBasicApp
{
    abstract class Person
    {
        protected string Name;
        public Person() { }
        public Person(string Name)
        {
            this.Name = Name;
        }
        public abstract void Describe();
    }
    class Student : Person
    {
        public Student() { }
        public Student(string Name) : base(Name)
        {
        }
        public override void Describe()
        {
            Console.WriteLine("Hi I am a student and my name is, " + Name);
        }
    }
    class Faculty : Person
    {
        public Faculty() { }
        public Faculty(string Name) : base(Name)
        {
        }
        public override void Describe()
        {
            Console.WriteLine("Hi I am a faculty and my name is, " + Name);
        }
    }
    internal class MyClassAbstract
    {
        static void Main(string[] args)
        {
            string role;
            role = Console.ReadLine();

            Person p;
            if(role == "Student")
            {
                p = new Student("Prajval");
            }
            else if(role == "Faculty")
            {
                p = new Faculty("Arya");
            }
            else
            {
                throw new ApplicationException("Provide valid role");
            }
            p.Describe();
        }
    }
}
