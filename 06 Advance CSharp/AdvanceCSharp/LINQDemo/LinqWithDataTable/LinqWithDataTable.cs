using LINQDemo.Model;
using System.Data;
using static LINQDemo.Utility.Utility;


namespace LINQDemo.LinqWithDataTable
{
    /// <summary>
    /// Class to demonstarte LINQ with Data Table
    /// </summary>
    internal class LinqWithDataTable
    {
        /// <summary>
        /// Method to demonstarte LINQ with Data Table
        /// </summary>
        public static void LinqWithDataTableMtd()
        {
            // create a data table
            DataTable tblEmployee = new DataTable("Employee");

            // create an id column for tblEmployee
            DataColumn columnId = new DataColumn();
            columnId.DataType = typeof(int);
            columnId.ColumnName = "id";
            columnId.Caption = "Employee Id";
            columnId.AutoIncrement = true;
            columnId.AutoIncrementSeed = 1;
            columnId.ReadOnly = false;
            columnId.Unique = true;

            // create a name column for employee table
            DataColumn columnName = new DataColumn();
            columnName.DataType = typeof(string);
            columnName.ColumnName = "name";
            columnName.Caption = "Employee Name";
            columnName.ReadOnly = false;
            columnName.Unique = false;

            // create a date of joining column for emplyee table
            DataColumn columnDateOfJoining = new DataColumn();
            columnDateOfJoining.DataType = typeof(DateOnly);
            columnDateOfJoining.ColumnName = "date_of_joining";
            columnDateOfJoining.Caption = "Employee Date of Joining";
            columnDateOfJoining.ReadOnly = false;
            columnDateOfJoining.Unique = false;

            // add above created data columns to tblEmployee
            tblEmployee.Columns.Add(columnId);
            tblEmployee.Columns.Add(columnName);
            tblEmployee.Columns.Add(columnDateOfJoining);

            // add id column as primary key
            tblEmployee.PrimaryKey = new DataColumn[]
            {
                tblEmployee.Columns["id"]
            };

            // create array of employee
            EMP01[] lstEmployee = new EMP01[]
            {
                new EMP01("Prajval Gahine", DateOnly.Parse("2022-03-19")),
                new EMP01("Yash Lathiya", DateOnly.Parse("2021-05-25")),
                new EMP01("Deep Patel", DateOnly.Parse("2022-03-15")),
                new EMP01("Krinsi Kayada", DateOnly.Parse("2023-01-08")),
            };

            // add employee row to tblEmployee
            AddRowToEmployeeTbl(tblEmployee, lstEmployee);

            // linq operations on data-table
            // data-table donot implement IEnumerable interface
            IEnumerable<EMP01> lstEmployeeFromDT = tblEmployee.AsEnumerable()
                .Select(row => new EMP01((int)row["id"], (string)row["name"], (DateOnly)row["date_of_joining"]))
                .Where(employee => employee.p01f03 > DateOnly.Parse("2023-01-07"));

            PrintEmployee(lstEmployeeFromDT.ToArray());
        }
    }
}
