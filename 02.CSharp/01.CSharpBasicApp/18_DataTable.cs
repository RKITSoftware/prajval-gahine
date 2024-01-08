using System;
using System.Data;

namespace CSharpBasicApp
{
    /// <summary>
    /// MyCLassDataTable class demonstrate DataTable in C#
    /// </summary>
    internal class MyClassDataTable
    {
        static void Main()
        {
            DataTable StudentDT = new DataTable("Student");
            StudentDT.Columns.Add("Id");
            StudentDT.Columns.Add("Name");
            StudentDT.Columns.Add("Grade");

            Console.WriteLine("Enter Id, Name and Grade seperated by commas: ");

            for(int i = 0; i < 5; i++)
            {

                // creating a new row
                DataRow row = StudentDT.NewRow();
                string data = Console.ReadLine();
                string[] values = data.Split(",");
                row["Id"]  = int.Parse(values[0]);
                row["Name"] = values[1];
                row["Grade"] = int.Parse(values[2]);

                // adding the newly created row to the StudentDT datatable
                StudentDT.Rows.Add(row);
            }

            Console.WriteLine("\nRetreving Records...\n");

            // retriving all records one by one from the StudentDT datatable
            foreach (DataRow row in StudentDT.Rows)
            {
                //Console.WriteLine(row["Id"] + " " + row["Name"] + " " + row["Grade"]);
                foreach (DataColumn col in StudentDT.Columns)
                {
                    Console.Write(row[col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
