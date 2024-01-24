using System.Data;

namespace LINQDemo
{
    internal partial class Program
    {
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

            // creating a tblEmployee row

        }
    }
}
