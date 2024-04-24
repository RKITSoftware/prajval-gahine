namespace FirmAdvanceDemo.Enums
{
    /// <summary>
    /// Represnt current status of leave. None value is used to get all types of leaves
    /// </summary>
    public enum EnmLeaveStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        P = 'P',

        /// <summary>
        /// Approved
        /// </summary>
        A = 'A',

        /// <summary>
        /// Rejected
        /// </summary>
        R = 'R',

        /// <summary>
        /// Expired
        /// </summary>
        E = 'E',

        /// <summary>
        /// All
        /// </summary>
        X = 'X',
    }

    /// <summary>
    /// Represnt type of punch
    /// </summary>
    public enum EnmPunchType : byte
    {
        /// <summary>
        /// Unprocessed
        /// </summary>
        U,

        /// <summary>
        /// Punch In
        /// </summary>
        I,

        /// <summary>
        /// Punch Out
        /// </summary>
        O,

        /// <summary>
        /// Mistaken Punch
        /// </summary>
        M,

        /// <summary>
        /// Ambiguous Punch
        /// </summary>
        A
    }

    /// <summary>
    /// Represent type of role of user
    /// </summary>
    public enum EnmRole
    {
        /// <summary>
        /// Admin
        /// </summary>
        A = 'A',

        /// <summary>
        /// Employee
        /// </summary>
        E = 'E'
    }

    public enum EnmGender : byte
    {
        /// <summary>
        /// Male
        /// </summary>
        M,

        /// <summary>
        /// Female
        /// </summary>
        F,

        /// <summary>
        /// Others
        /// </summary>
        O
    }

    /// <summary>
    /// Represent type of operation to be performed
    /// </summary>
    public enum EnmOperation : byte
    {
        /// <summary>
        /// Add
        /// </summary>
        A,

        /// <summary>
        /// Edit
        /// </summary>
        E
    }
}