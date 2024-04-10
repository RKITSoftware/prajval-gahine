
using LINQDemo.Model;
using System.Data;

namespace LINQDemo.Utility
{
    /// <summary>
    /// A utility class for displaying .net objects in proper format
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// A method to print number list with message in proper format
        /// </summary>
        /// <param name="lstNums">List of number</param>
        /// <param name="message">Message</param>
        public static void PrintArray(int[] lstNums, string message)
        {
            Console.Write(message + ": ");
            foreach (int n in lstNums)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// A method to print person list with message in proper format
        /// </summary>
        /// <param name="lstNums">List of person</param>
        /// <param name="message">Message</param>
        public static void PrintPersonArray(PSN01[] lstPerson, string message)
        {
            Console.WriteLine(message + ": ");
            foreach (PSN01 person in lstPerson)
            {
                Console.WriteLine($"{person.n01f01}\t{person.n01f02}\t{person.n01f03}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Method to add items of employee list to employee table as table row
        /// </summary>
        /// <param name="tblEmployee">employee table</param>
        /// <param name="lstEmployee">employee list</param>
        public static void AddRowToEmployeeTbl(DataTable tblEmployee, EMP01[] lstEmployee)
        {
            foreach (EMP01 employee in lstEmployee)
            {
                DataRow rowEmployee = tblEmployee.NewRow();
                rowEmployee["name"] = employee.p01f02;
                rowEmployee["date_of_joining"] = employee.p01f03;
                tblEmployee.Rows.Add(rowEmployee);
            }
        }

        /// <summary>
        /// Method to print employee details from employee list
        /// </summary>
        /// <param name="lstEmployee">employee list</param>
        public static void PrintEmployee(EMP01[] lstEmployee)
        {
            foreach (EMP01 employee in lstEmployee)
            {
                Console.WriteLine($"Employee Id: {employee.p01f01}, name: {employee.p01f02}, doj: {employee.p01f03}");
            }
        }
    }
}
