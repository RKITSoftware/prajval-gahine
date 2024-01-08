using SalaryManagement;

namespace FirmManagementApplication
{
    /// <summary>
    /// Adminstration represents admin in the firm
    /// </summary>
    internal class Adminstrator : User
    {
        #region StaticMethods
        /// <summary>
        /// Start method is entry point for performing opertaion on behalf of admin
        /// </summary>
        public static void Start()
        {
            const string Role = "Adminstrator";

            Console.Write("\nEnter admin username: ");
            string AdminUserName = Console.ReadLine();

            Console.Write("Enter admin password: ");
            string AdminPassword = Console.ReadLine();

            string[] AdminData;

            // authenticate and if authenticated, create an Administrator instance
            Adminstrator Admin = (Adminstrator)Adminstrator.AuthenticateUser(Role, AdminUserName, AdminPassword, out AdminData);

            Console.WriteLine("\nWelcome Admin, {0}", Admin.Name);
            while (true)
            {
                Console.WriteLine("\nAdmin Actions:");
                Console.WriteLine("1. Show all Employees\n2. Add an Employee\n3. Remove an Employee\n4. Credit Salaries");

                Console.Write("\nChoose action: ");
                byte ActionChoiceNo;
                byte.TryParse(Console.ReadLine(), out ActionChoiceNo);

                switch (ActionChoiceNo)
                {
                    case 1:
                        Admin.ShowAllEmployees();
                        break;
                    case 2:
                        Admin.AddEmployee();
                        break;
                    case 3:
                        Console.Write("\nEnter the Username of Employee to remove: ");
                        string EmployeeToRemove = Console.ReadLine();
                        Admin.RemoveEmployee(EmployeeToRemove);
                        break;
                    case 4:
                        Salary.CreditAllEmployeesSalary();
                        break;
                    default:
                        break;
                }
                Console.Write("Do you want to logout of Admin's session? yes or no: ");
                string WantToLogout = Console.ReadLine().ToLower();
                if(WantToLogout == "yes")
                {
                    break;
                }
            }
        }
        #endregion

        #region InstanceMethods
        /// <summary>
        /// Lists out all the employee from the Employee.csv data-source on to the console
        /// </summary>
        public void ShowAllEmployees()
        {
            // access the Employees.csv file and show it on console
            const string EmployeesCsvFilePath = @"Data Sources\Employee.csv";
            string[] EmployeeRecords = File.ReadAllLines(EmployeesCsvFilePath);

            // console formatters
            const string FormatString = "| {0, -5} | {1, -10} | {2, -15} | {3, -15} | {4, -30} | {5, -15} | {6, -15} | {7, -20} | {8, -10} |";
            string HorizontalLine = new string('-', 163);

            Console.WriteLine("\nAll Employees List");
            Console.WriteLine(HorizontalLine);
            Console.WriteLine(FormatString, "Id", "Username", "Name", "Phone Number", "Email", "Date Of Joining", "Department", "Position", "Project Id");
            Console.WriteLine(HorizontalLine);

            for (int i = 1; i < EmployeeRecords.Length; i++)
            {
                List<string> EmployeeData = EmployeeRecords[i].Split(',').ToList();
                EmployeeData.RemoveAt(2);
                Console.WriteLine(FormatString, EmployeeData[0], EmployeeData[1], EmployeeData[2], EmployeeData[3], EmployeeData[4], EmployeeData[5], EmployeeData[6], EmployeeData[7], EmployeeData[8]);
            }
            Console.WriteLine(HorizontalLine);
            Console.WriteLine();
        }

        /// <summary>
        /// Adds a new employee entry to the Employee.csv data-source
        /// </summary>
        /// <exception cref="UserAlreayExitsException">This exception is thrown when the user is already present in the data-source</exception>
        public void AddEmployee()
        {
            const string MetadataFilePath = @"Data Sources\Metadata.csv";

            Console.Write("Enter Employee Username: ");
            string EmployeeUsername = Console.ReadLine();

            // check if user already exits
            bool DoesUserAlreadyExists = Adminstrator.DoesUserExists("Employee", EmployeeUsername);

            if (DoesUserAlreadyExists)
            {
                throw new UserAlreayExitsException(string.Format("{0} - user already exists", EmployeeUsername));
            }

            // if user not exits then, take the input for employee data source
            Console.WriteLine("Enter Employee Password,Name,Phone Number,Email,Department,Position,Project Id,IsPreviousMothSalaryCredited,Salary (separeted by comma)");
            List<string> NewEmployeeData = Console.ReadLine().Split(',').ToList();
            string[] MetadataFileLines = File.ReadAllLines(MetadataFilePath);
            string EmployeeId = MetadataFileLines[2].Split(',')[0];
            NewEmployeeData.Insert(0, EmployeeId);
            NewEmployeeData.Insert(1, EmployeeUsername);
            NewEmployeeData.Insert(6, DateTime.Now.ToString("dd-MM-yyyy"));

            const string EmployeeCsvFilePath = @"Data Sources\Employee.csv";
            StreamWriter sw = new StreamWriter(EmployeeCsvFilePath, true);
            string NewEmployeeDataString = string.Join(',', NewEmployeeData);
            sw.WriteLine(NewEmployeeDataString);
            sw.Close();

            // update the EmployeeNextId in Metadata.csv data source
            int EmployeeNextId = int.Parse(MetadataFileLines[2].Split(',')[0]);
            int NoOfEmployees = int.Parse(MetadataFileLines[2].Split(',')[1]);

            string UpdatedRecordString = String.Format("{0},{1}", ++EmployeeNextId, ++NoOfEmployees);
            MetadataFileLines[2] = UpdatedRecordString;

            File.WriteAllLines(MetadataFilePath, MetadataFileLines);
        }

        /// <summary>
        /// removes the user based on username criteria.
        /// </summary>
        /// <param name="Username">string that will be used to search for the user in the data-source againt it's username attribute</param>
        /// <exception cref="InvalidUserNameException">This exception is thrown when the user to be removed is not present in the data-source</exception>
        public void RemoveEmployee(string Username)
        {
            const string Role = "Employee";
            bool IsUserPresent = DoesUserExists(Role, Username);

            string UserCsvFilePath = string.Format(@"Data Sources\{0}.csv", Role);
            List<string> UserRecords = File.ReadAllLines(UserCsvFilePath).ToList();

            int IndexOfRecordToRemove = -1;
            int EmployeeIdOfUserToRemove = 0;
            int DaySalary = -1;

            for(int i = 1; i < UserRecords.Count; i++)
            {
                string[] UserData = UserRecords[i].Split(',');
                if (Username == UserData[1])
                {
                    // user exsits, hence remove this record
                    IndexOfRecordToRemove = i;
                    EmployeeIdOfUserToRemove = int.Parse(UserData[0]);
                    DaySalary = int.Parse(UserData[11]);
                }
            }
            if(IndexOfRecordToRemove == -1)
            {
                throw new InvalidUserNameException("User doesnot exists");
            }
            // employee removed from EmployeeRecords
            UserRecords.RemoveAt(IndexOfRecordToRemove);

            // update the new Employee list to the data-source([User].csv)
            File.WriteAllLines(UserCsvFilePath, UserRecords);

            // credit salary of removd user and remove all the attendances

            // credit it's this month salary
            Salary.CreditSalariesOfAnEmployee(EmployeeIdOfUserToRemove, DaySalary);

            // remove the corresponding record of removed employee from Attendance.csv
            const string AttendanceCsvFilePath = @"Data Sources\Attendance.csv";
            List<string> AttendanceRecords = File.ReadAllLines(AttendanceCsvFilePath).ToList();

            List<int> AttendanceIndexToRemove = new List<int>();

            for(int i = 1; i < AttendanceRecords.Count; i++)
            {
                string[] AttendanceData = AttendanceRecords[i].Split(",");
                if (EmployeeIdOfUserToRemove == int.Parse(AttendanceData[1]))
                {
                    AttendanceIndexToRemove.Add(i);
                }
            }

            int j = 0;
            foreach(int IndexToRemove in AttendanceIndexToRemove)
            {
                AttendanceRecords.RemoveAt(IndexToRemove-j);
                j++;
            }

            // update the Attendance.csv file
            File.WriteAllLines(AttendanceCsvFilePath, AttendanceRecords);

            // update the NoOfRecords of Employee field in Metadata.csv file
            const string MetadataCsvFilePath = @"Data Sources\MetaData.csv";
            string[] MetadataRecords = File.ReadAllLines(MetadataCsvFilePath);

            string[] EmployeeMetadata = MetadataRecords[2].Split(',');
            string[] AttendanceMetadata = MetadataRecords[4].Split(",");

            int NoOfEmployeeRecords = int.Parse(EmployeeMetadata[1]);
            int NoOfAttendanceRecords = int.Parse(AttendanceMetadata[1]);

            int NoOfAttendanceRecordRemoved = AttendanceIndexToRemove.Count;
            string NewEmployeeMetadataRecord = string.Format("{0},{1}", EmployeeMetadata[0], --NoOfEmployeeRecords);;
            string NewAttendanceMetadataRecord = string.Format("{0},{1}", AttendanceMetadata[0], NoOfAttendanceRecords - NoOfAttendanceRecordRemoved);;

            MetadataRecords[2] = NewEmployeeMetadataRecord;
            MetadataRecords[4] = NewAttendanceMetadataRecord;

            // write the new Metadata records into Metadata.csv
            File.WriteAllLines(MetadataCsvFilePath, MetadataRecords);


            // writing to console that intended [user] was successfully removed
            Console.WriteLine("{0} {1} was removed successfully", Username, Role);
        }
        #endregion
    }

    /// <summary>
    /// This exception is thrown when user supplies an invalid user-name
    /// </summary>
    internal class UserAlreayExitsException : Exception
    {
        public UserAlreayExitsException() : base() { }
        public UserAlreayExitsException(string Message) : base(Message) { }
        public UserAlreayExitsException(string Message, Exception InnerException) : base(Message, InnerException) { }
    }
}
