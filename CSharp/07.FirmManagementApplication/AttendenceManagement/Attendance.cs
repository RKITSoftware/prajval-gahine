namespace AttendanceManagement
{
    /// <summary>
    /// Attendance class helps to create Attendance object of an Employee
    /// </summary>
    public sealed class Attendance
    {
        #region Fields
        /// <summary>
        /// Attendance object id
        /// </summary>
        private int _Id;
        /// <summary>
        /// Employee id whose attendance is to be filled
        /// </summary>
        private int _EmployeeId;
        /// <summary>
        /// Date of attendance
        /// </summary>
        private DateOnly _AttendanceDate;
        /// <summary>
        /// Type of work done on that date
        /// </summary>
        private DayWorkType _WorkType;
        #endregion

        #region Properties
        public int EmployeeId
        {
            get
            {
                return _EmployeeId;
            }
            set
            {
                if(value <= 0)
                {
                    throw new Exception("Id cannot be zero or negative");
                }
                _EmployeeId = value;
            }
        }
        //public DateOnly AttendanceDate
        //{
        //    get
        //    {
        //        return _AttendanceDate;
        //    }
        //    set
        //    {
        //        _AttendanceDate = value;
        //    }
        ////}
        public DayWorkType WorkType
        {
            get
            {
                return _WorkType;
            }
            set
            {
                _WorkType = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Attendance instance and saves it to csv if attendance is not filled for the current date
        /// </summary>
        /// <param name="EmployeeId">Id of an Employee whose attendance is to be filled</param>
        /// <param name="WorkType">WorkType describes the type of work for the current date done by the employee. i.e. Half-day or Full-day</param>
        public Attendance(int EmployeeId, DayWorkType WorkType)
        {
            // set Id of Attendance, get the next id from Metadata.csv data-source
            const string MatadataCsvFilePath = @"Data Sources\Metadata.csv";
            string[] MetadataRecords = File.ReadAllLines(MatadataCsvFilePath);
            string[] AttendanceRecord = MetadataRecords[4].Split(',');
            int AttendanceNextId = int.Parse(AttendanceRecord[0]);
            int NoOfAttendanceRecords = int.Parse(AttendanceRecord[1]);

            this._Id = AttendanceNextId;

            this.EmployeeId = EmployeeId;

            this._AttendanceDate = DateOnly.FromDateTime(DateTime.Now);

            this.WorkType = WorkType;

            this.SaveAttendanceToCsv();

            // update the AttendanceNextId and NoOfAttendanceRecords value in Metadata.csv data-source
            MetadataRecords[4] = string.Format("{0},{1}", ++AttendanceNextId, ++NoOfAttendanceRecords);
            // save the changes to Metadata.csv
            File.WriteAllLines(MatadataCsvFilePath, MetadataRecords);
        }
        #endregion

        #region InstanceMethods
        /// <summary>
        /// If the user has not filled current date attendance then this method saves the attendance object to the Attendance.csv data-source
        /// </summary>
        private void SaveAttendanceToCsv()
        {
            const string AttendanceCsvFilePath = @"Data Sources\Attendance.csv";

            if (!this.IsAttendanceAlreadyFilled())
            {
                // generate string-comma-seperate attendance-record from the Attendance Object
                string AttendanceRecord = string.Format("{0},{1},{2},{3}\n", this._Id, this._EmployeeId, this._AttendanceDate.ToString(), (byte)this.WorkType);

                // save it to the Attendance.csv data-source
                File.AppendAllText(AttendanceCsvFilePath, AttendanceRecord);
            }
        }

        /// <summary>
        /// Checks if the Employee has already filled the attendance of the current date
        /// </summary>
        /// <returns>false if attendance is not filled for current date else throws an exception</returns>
        /// <exception cref="Exception">If the user has already filled current date attendance then this Exception is thrown</exception>
        private bool IsAttendanceAlreadyFilled()
        {
            const string AttendanceCsvFilePath = @"Data Sources\Attendance.csv";

            // fetch the Attendance.csv data-source content
            string[] AttendanceRecords = File.ReadAllLines(AttendanceCsvFilePath);

            // check if any record has "EmployeeId AND AttendanceDate" same, if yes then throw an error
            for (int i = 1; i < AttendanceRecords.Length; i++)
            {
                string[] AttendanceData = AttendanceRecords[i].Split(',');

                // EmployeeId is at index 1 and Date is at index 2
                if ((this._EmployeeId == int.Parse(AttendanceData[1])) && (this._AttendanceDate.ToString() == AttendanceData[2]))
                {
                    throw new Exception("Attendance already filled");
                }
            }
            return false;
        }
        #endregion
    }

    /// <summary>
    /// DayWorkType represents the work done by the employee for a particular day. 1 represent Half-day work and 2 represnet Full-Day work
    /// </summary>
    public enum DayWorkType : byte
    {
        HalfDay = 1,
        FullDay = 2
    }
}