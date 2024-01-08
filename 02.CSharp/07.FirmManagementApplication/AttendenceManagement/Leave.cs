using System;

namespace AttendanceManagement
{
    /// <summary>
    /// Leave reprsent an object that can be used(sought) by an employee
    /// </summary>
    public class Leave
    {
        /// <summary>
        /// Leave object id
        /// </summary>
        private int _Id;
        /// <summary>
        /// Employee id whose leave is to be granted
        /// </summary>
        private int _EmployeeId;
        /// <summary>
        /// Date on which the leave has been requested
        /// </summary>
        private DateTime _LeaveRequestDateTime;
        /// <summary>
        /// Date for which leave is requested
        /// </summary>
        private DateOnly _LeaveDate;
        /// <summary>
        /// Description of the leave
        /// </summary>
        private string _Description;
        /// <summary>
        /// Type of leave
        /// </summary>
        private LeaveType _LeaveType;

        /// <summary>
        /// Leave constructor create and initialises Leave object
        /// </summary>
        /// <param name="EmployeeId">Represent Employee whose Leave is to be granted</param>
        /// <param name="LeaveDate">Date at which leave is requested</param>
        /// <param name="Description">Leave description</param>
        /// <param name="LeaveType">Type of Leave</param>
        /// <exception cref="Exception"></exception>
        public Leave(int EmployeeId, DateOnly LeaveDate, string Description, LeaveType LeaveType)
        {
            // set Id of Leave, get the next id from Metadata.csv data-source
            const string MatadataCsvFilePath = @"Data Sources\Metadata.csv";
            string[] MetadataRecords = File.ReadAllLines(MatadataCsvFilePath);
            string[] LeaveRecord = MetadataRecords[5].Split(',');
            int LeaveNextId = int.Parse(LeaveRecord[0]);
            int NoOfLeaveRecords = int.Parse(LeaveRecord[1]);

            this._Id = LeaveNextId;

            this._EmployeeId = EmployeeId;

            this._LeaveRequestDateTime = DateTime.Now;

            this._LeaveDate = LeaveDate;

            this._Description = Description;

            this._LeaveType = LeaveType;

            this.GrantLeave();

            // update the LeaveNextId and NoOfLeaveRecords value in Metadata.csv data-source
            MetadataRecords[5] = string.Format("{0},{1}", ++LeaveNextId, ++NoOfLeaveRecords);
            // save the changes to Metadata.csv
            File.WriteAllLines(MatadataCsvFilePath, MetadataRecords);
        }

        /// <summary>
        /// GrantLeave method stores the Leave information from Leave-object to Leave.csv data-source
        /// </summary>
        private void GrantLeave()
        {
            string LeaveCsvFilePath = @"Data Sources\Leave.csv";

            // convert Leave-object to csv record string format
            string LeaveCsvRecordString = this.ConvertLeaveObjectToCsvRecordString();

            // write the Leave-object-string to the Leave.csv data-source
            File.AppendAllText(LeaveCsvFilePath, LeaveCsvRecordString);
        }

        /// <summary>
        /// Converts a Leave object into string that is compatible for Leave.csv file record
        /// </summary>
        /// <returns>A string that is compatible for Leave.csv file record</returns>
        private string ConvertLeaveObjectToCsvRecordString()
        {
            string LeaveCsvRecordString = string.Format("{0},{1},{2},{3},{4},{5}\n",
                this._Id,
                this._EmployeeId,
                this._LeaveRequestDateTime.ToString(),
                this._LeaveDate.ToString(),
                this._LeaveType,
                this._Description);
            return LeaveCsvRecordString;
        }
    }

    /// <summary>
    /// LeaveType enum represent type of leave (sick, emergency, casual)
    /// </summary>
    public enum LeaveType
    {
        Sick = 1,
        Emergency,
        Casual
    }
}