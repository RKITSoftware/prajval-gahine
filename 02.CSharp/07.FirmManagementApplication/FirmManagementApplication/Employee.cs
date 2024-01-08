
using AttendanceManagement;

namespace FirmManagementApplication
{
    /// <summary>
    /// Employee represents an employee in the firm
    /// </summary>
    internal class Employee : User
    {
        #region Fields
        /// <summary>
        /// Employee's date of joining
        /// </summary>
        private DateOnly _DateOfJoining;
        /// <summary>
        /// Employee's department
        /// </summary>
        private string _Department;
        /// <summary>
        /// Employee's position
        /// </summary>
        private string _Position;
        /// <summary>
        /// Employee's current working project id
        /// </summary>
        private int _ProjectId;
        #endregion

        #region StaticMethods
        /// <summary>
        /// Start method is entry point for performing opertaion on behalf of employee
        /// </summary>
        internal static void Start()
        {
            const string Role = "Employee";

            Console.Write("\nEnter employee username: ");
            string EmployeeUsername = Console.ReadLine();

            Console.Write("Enter employee password: ");
            string EmployeePassword = Console.ReadLine();

            string[] EmployeeData;

            // authenticate and if authenticated, create an Employee instance
            Employee Employee = (Employee)Employee.AuthenticateUser(Role, EmployeeUsername, EmployeePassword, out EmployeeData);

            // populating employee specific data into Employee object
            Employee._DateOfJoining = DateOnly.FromDateTime(DateTime.Parse(EmployeeData[6]));
            Employee._Department = EmployeeData[7];
            Employee._Position = EmployeeData[8];
            Employee._ProjectId = int.Parse(EmployeeData[9]);

            Console.WriteLine("\nWelcome Employee, {0}", Employee.Name);
            while (true)
            {
                Console.WriteLine("\nEmployee Actions:");
                Console.WriteLine("1. Fill Today's Attendance\n2. Request For Leave");

                Console.Write("\nChoose action: ");
                byte ActionChoiceNo;
                byte.TryParse(Console.ReadLine(), out ActionChoiceNo);

                switch (ActionChoiceNo)
                {
                    case 1:
                        Employee.FillTodaysAttendance();
                        break;
                    case 2:
                        Employee.RequestALeave();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Do you want to logout of Employee's session? yes or o/w");
                string WantToLogout = Console.ReadLine().ToLower();
                if (WantToLogout == "yes")
                {
                    break;
                }
            }
        }
        #endregion

        #region InstanceMethods
        /// <summary>
        /// Fills todays attendance for the employee
        /// </summary>
        internal void FillTodaysAttendance()
        {
            Console.WriteLine("\nWork Types:\n1. Half Day\n2. Full Day");
            Console.Write("Choose a work type: ");
            DayWorkType WorkType = (DayWorkType)byte.Parse(Console.ReadLine());

            Attendance EmployeeAttendance;
            try
            {
                // create attendance-object and doing so it will save to Attendance.csv data-source
                EmployeeAttendance = new Attendance(this.Id, WorkType);
                Console.WriteLine("Your attendance was filled successfully for today");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Method to request a leave of employee
        /// </summary>
        public void RequestALeave()
        {
            Console.Write("\nEnter leave date in dd-mm-yyyy format: ");
            string leaveDateString = Console.ReadLine();

            DateOnly LeaveDate = DateOnly.FromDateTime(DateTime.Parse(leaveDateString));

            if (LeaveDate < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Leave date cannot be less than current date");
            }
            if (LeaveDate > DateOnly.FromDateTime(DateTime.Now).AddDays(5))
            {
                throw new Exception("Can't seek a leave before 5 days.");
            }

            Console.WriteLine("Leave Choices:\n1. Sick\n2. Emergency\n3. Casual");
            Console.Write("Choose Leave Type: ");
            LeaveType LeaveType = (LeaveType)int.Parse(Console.ReadLine());

            Console.WriteLine("Give description of the leave:");
            string Description = Console.ReadLine();


            Leave Leave = new Leave(102, LeaveDate, Description, LeaveType);
        }
        #endregion
    }
}
