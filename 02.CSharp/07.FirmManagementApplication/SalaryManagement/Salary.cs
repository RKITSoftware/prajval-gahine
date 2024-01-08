using System.Collections;

namespace SalaryManagement
{
    /// <summary>
    /// Salary represent an employee's moth salary
    /// </summary>
    public class Salary
    {
        #region Fields
        /// <summary>
        /// SalaryNextId of Metadata.csv data-source
        /// </summary>
        public static int SalaryNextId;
        /// <summary>
        /// NoOfSalaryRecords of Metadata.csv data-source
        /// </summary>
        public static int NoOfSalaryRecords;
        /// <summary>
        /// Salary credit date for each month credition
        /// </summary>
        public static byte SalaryCreditDate = 1;
        /// <summary>
        /// Salary object id
        /// </summary>
        private int _Id;
        /// <summary>
        /// Employee id whose salary is to be credited
        /// </summary>
        private int _EmployeeId;
        /// <summary>
        /// Month for which the salary is to be credited
        /// </summary>
        private byte _Month;
        /// <summary>
        /// Year for which the salary is to be credited
        /// </summary>
        private int _Year;
        /// <summary>
        /// Salary amount of employee's month salary
        /// </summary>
        private float _Amount;
        #endregion

        /// <summary>
        /// This static constructor is used to initialize SalaryNextId and NoOfSalaryRecords from Metadata.csv file
        /// </summary>
        static Salary()
        {
            // get the next id from Metadata.csv data-source and set it to SalaryNextId static variable
            const string MatadataCsvFilePath = @"Data Sources\Metadata.csv";
            string[] MetadataRecords = File.ReadAllLines(MatadataCsvFilePath);
            string[] SalaryRecord = MetadataRecords[6].Split(',');
            Salary.SalaryNextId = int.Parse(SalaryRecord[0]);
            Salary.NoOfSalaryRecords = int.Parse(SalaryRecord[1]);
        }
        #region Constructors
        /// <summary>
        /// This overloaded constructor creates and initialize a Salary Instance
        /// </summary>
        /// <param name="EmployeeId">Represent an Employee in Employee.csv data-soruce</param>
        /// <param name="Month">Previous Month byte value</param>
        /// <param name="Year">Current or Previous year value</param>
        /// <param name="Amount">Amount the is to be credited for the employee</param>
        public Salary(int EmployeeId, byte Month, int Year, float Amount)
        {
            this._Id = Salary.SalaryNextId;

            this._EmployeeId = EmployeeId;

            this._Month = Month;

            this._Year = Year;

            this._Amount = Amount;

            Salary.SalaryNextId++;
            Salary.NoOfSalaryRecords++;
        }
        #endregion

        #region StaticMethods
        /// <summary>
        /// This static method credits previous month salary of all working employees
        /// </summary>
        public static void CreditAllEmployeesSalary()
        {
            DateTime TodaysDate = DateTime.Now;
            int TodaysDay = TodaysDate.Day;

            if(TodaysDay == Salary.SalaryCreditDate)
            {
                // get the attendance of all employee (Attendance.csv)
                const string AttendanceCsvFilePath = @"Data Sources\Attendance.csv";
                string[] AttendanceRecords = File.ReadAllLines(AttendanceCsvFilePath);

                // get Employee.csv file
                const string EmployeeCsvFilePath = @"Data Sources\Employee.csv";
                string[] EmployeeRecords = File.ReadAllLines(EmployeeCsvFilePath);

                Hashtable EmployeeRecordsHashTable = new Hashtable(EmployeeRecords.Length-1);
                for(int i = 1; i < EmployeeRecords.Length; i++)
                {
                    string[] EmployeeData = EmployeeRecords[i].Split(',');
                    EmployeeRecordsHashTable.Add(int.Parse(EmployeeData[0]), EmployeeData);
                }

                Dictionary<int, int> EmployeeWorkDayCounts = new Dictionary<int, int>(EmployeeRecords.Length - 1);

                DateTime PreviousMonthFirstDate;
                DateTime PreviousMonthLastDate;

                TimeSpan MinusOneDay = new TimeSpan(-1, 0, 0, 0);

                TimeSpan TimeSpanForLastDate = new TimeSpan(0, TodaysDate.Hour, TodaysDate.Minute, TodaysDate.Second+1, 0);
                TimeSpan TimeSpanForFirstDate = new TimeSpan(0, 23, 59, 59, 999);
                PreviousMonthLastDate = TodaysDate.Subtract(TimeSpanForLastDate);
                PreviousMonthFirstDate = PreviousMonthLastDate.AddDays(-(PreviousMonthLastDate.Day - 1)).Subtract(TimeSpanForFirstDate);

                // Now traverse attendance-records and populate the value of the dicitionary
                for (int i = 1; i < AttendanceRecords.Length; i++)
                {
                    string[] AttendanceData = AttendanceRecords[i].Split(',');
                    string AttendanceDateString = AttendanceData[2];
                    DateTime AttendanceDate = DateTime.Parse(AttendanceDateString);

                    if ((PreviousMonthFirstDate <= AttendanceDate) && (AttendanceDate <= PreviousMonthLastDate))
                    {
                        int EmployeeId = int.Parse(AttendanceData[1]);
                        int DayCount = int.Parse(AttendanceData[3]);
                        int CummulativeDayCount = 0;
                        EmployeeWorkDayCounts.TryGetValue(EmployeeId, out CummulativeDayCount);
                        EmployeeWorkDayCounts[EmployeeId] = CummulativeDayCount + DayCount;
                    }
                }

                List<Salary> PreviousMonthSalaries = new List<Salary>(EmployeeWorkDayCounts.Count);

                // as an employee work type credit the salary of a month to their account
                foreach (KeyValuePair<int,int> kvp in EmployeeWorkDayCounts)
                {
                    string[] EmployeeRecord = (EmployeeRecordsHashTable[kvp.Key] as string[]);
                    float Amount = (kvp.Value / 2.0F) * float.Parse(EmployeeRecord[11]);

                    Salary Salary = new Salary(kvp.Key, (byte)PreviousMonthFirstDate.Month, PreviousMonthFirstDate.Year, Amount);
                    PreviousMonthSalaries.Add(Salary);

                    Console.WriteLine("Employee with ID {0} has been credited with a salary of {1}", kvp.Key, Amount);
                }

                // invoke SaveSalaryRecordsToCsv
                Salary.SaveSalaryRecordsToCsv(PreviousMonthSalaries);

            }
            else
            {
                Console.WriteLine("Today is not month first day. Cannot credit salaries");
            }
        }

        /// <summary>
        /// Method saves given List of Salary instances to the Salary.csv data-source
        /// </summary>
        /// <param name="PreviousMonthSalaries"></param>
        public static void SaveSalaryRecordsToCsv(List<Salary> PreviousMonthSalaries)
        {
            const string SalaryCsvFilePath = @"Data Sources\Salary.csv";
            string[] SalaryRecords = new string[PreviousMonthSalaries.Count];

            int i = 0;
            foreach(Salary EmployeeSalaryDetail in PreviousMonthSalaries)
            {
                string SalaryCsvRecordString = EmployeeSalaryDetail.ConvertSalaryObjectToCsvRecordString();
                SalaryRecords[i++] = SalaryCsvRecordString;
            }

            // appending all previous month salary to Salary.csv file
            File.AppendAllLines(SalaryCsvFilePath, SalaryRecords);

            // flush the data in Attendance.csv file
            const string AttendanceCsvFilePath = @"Data Sources\Attendance.csv";
            string[] AttendanceRecords = File.ReadAllLines(AttendanceCsvFilePath);
            File.WriteAllText(AttendanceCsvFilePath, AttendanceRecords[0] + "\n");

            // updating the Metadata.csv file
            const string MatadataCsvFilePath = @"Data Sources\Metadata.csv";
            string[] MetadataRecords = File.ReadAllLines(MatadataCsvFilePath);

            // update the LeaveNextId and NoOfLeaveRecords value in Metadata.csv data-source
            MetadataRecords[6] = string.Format("{0},{1}", ++SalaryNextId, ++NoOfSalaryRecords);

            // update the Attendance record of Metadata.csv file
            MetadataRecords[4] = string.Format("101,0");

            // save the changes to Metadata.csv
            File.WriteAllLines(MatadataCsvFilePath, MetadataRecords);
        }

        /// <summary>
        /// Methods credits salaries of an employee
        /// </summary>
        /// <param name="EmployeeId">Employee of Id whose salary is to be credited</param>
        /// <param name="DaySalary">Per day Salary of Employee</param>
        public static void CreditSalariesOfAnEmployee(int EmployeeId, int DaySalary)
        {
            const string AttendanceCsvFilePath = @"Data Sources\Attendance.csv";
            string[] AttendanceRecords = File.ReadAllLines(AttendanceCsvFilePath);

            int DaysCount = 0;

            for(int i = 1; i < AttendanceRecords.Length; i++)
            {
                string[] AttendanceData = AttendanceRecords[i].Split(',');

                if(EmployeeId == int.Parse(AttendanceData[1]))
                {
                    DaysCount += int.Parse(AttendanceData[3]);
                }
            }

            float Amount = (DaysCount / 2.0F) * DaySalary;

            if(Amount > 0)
            {
                DateTime TodaysDate = DateTime.Now;
                Salary Salary = new Salary(EmployeeId, (byte)TodaysDate.Month, DateTime.Now.Year, Amount);
                Salary.SaveSalaryRecordToCsv();

                Console.WriteLine("Employee with ID {0} has been credited with a salary of {1}", EmployeeId, Amount);
            }            
        }
        #endregion

        #region InstanceMethods
        /// <summary>
        /// Method converts a salary object to record-string for Salary.csv file
        /// </summary>
        /// <returns>returns a record-string for Salary.csv file</returns>
        public string ConvertSalaryObjectToCsvRecordString()
        {
            string SalaryCsvRecordString = string.Format("{0},{1},{2},{3},{4}",                
                this._Id,
                this._EmployeeId,
                this._Month,
                this._Year,
                this._Amount                
                );
            return SalaryCsvRecordString;
        }


        /// <summary>
        /// Saves a salary instance to Salary.csv file and updates the Metadata.csv file
        /// </summary>
        public void SaveSalaryRecordToCsv()
        {
            string SalaryRecordString = string.Format("{0},{1},{2},{3},{4}\n",
                this._Id,
                this._EmployeeId,
                this._Month,
                this._Year,
                this._Amount
                );
            const string SalaryCsvFilePath = @"Data Sources\Salary.csv";
            File.AppendAllText(SalaryCsvFilePath, SalaryRecordString);

            // updating the Metadata.csv file
            const string MatadataCsvFilePath = @"Data Sources\Metadata.csv";
            string[] MetadataRecords = File.ReadAllLines(MatadataCsvFilePath);

            // update the LeaveNextId and NoOfLeaveRecords value in Metadata.csv data-source
            MetadataRecords[6] = string.Format("{0},{1}", ++SalaryNextId, ++NoOfSalaryRecords);
        }
        #endregion
    }
}