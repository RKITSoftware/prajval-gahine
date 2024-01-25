using System.Data;
using System.Linq;
using LINQDemo.Model;


namespace LINQDemo
{
    internal partial class Program
    {
        public static void AddRowToEmployeeTbl(DataTable tblEmployee, Employee[] lstEmployee)
        {
            foreach (Employee employee in lstEmployee)
            {
                DataRow rowEmployee = tblEmployee.NewRow();
                rowEmployee["name"] = employee.name;
                rowEmployee["date_of_joining"] = employee.date_of_joining;
                tblEmployee.Rows.Add(rowEmployee);
            }
        }

        public static void PrintEmployee(Employee[] lstEmployee)
        {
            foreach(Employee employee in lstEmployee)
            {
                Console.WriteLine($"Employee Id: {employee.id}, name: {employee.name}, doj: {employee.date_of_joining}");
            }
        }

        public static void LinqWithDataTable()
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
            Employee[] lstEmployee = new Employee[]
            {
                new Employee("Prajval Gahine", DateOnly.Parse("2022-03-19")),
                new Employee("Yash Lathiya", DateOnly.Parse("2021-05-25")),
                new Employee("Deep Patel", DateOnly.Parse("2022-03-15")),
                new Employee("Krinsi Kayada", DateOnly.Parse("2023-01-08")),
            };

            // add employee row to tblEmployee
            AddRowToEmployeeTbl(tblEmployee, lstEmployee);

            // linq operations on data-table
            // data-table donot implement IEnumerable interface
            IEnumerable<Employee> lstEmployeeFromDT = tblEmployee.AsEnumerable()
                .Select(row => new Employee((int)row["id"], (string)row["name"], (DateOnly)row["date_of_joining"]))
                .Where(employee => employee.date_of_joining > DateOnly.Parse("2023-01-07"));

            PrintEmployee(lstEmployeeFromDT.ToArray<Employee>());
        }
    }
}
