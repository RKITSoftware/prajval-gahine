namespace FirmAdvanceDemo.Enums
{
    /// <summary>
    /// Represnt current status of leave. None value is used to get all types of leaves
    /// </summary>
    public enum EnmLeaveStatus : byte
    {
        /// <summary>
        /// Pending
        /// </summary>
        P,

        /// <summary>
        /// Approved
        /// </summary>
        A,

        /// <summary>
        /// Rejected
        /// </summary>
        R,

        /// <summary>
        /// Expired
        /// </summary>
        E,

        /// <summary>
        /// Historic
        /// </summary>
        H,
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
    public enum EnmRole : byte
    {
        /// <summary>
        /// Admin
        /// </summary>
        A,

        /// <summary>
        /// Employee
        /// </summary>
        E
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