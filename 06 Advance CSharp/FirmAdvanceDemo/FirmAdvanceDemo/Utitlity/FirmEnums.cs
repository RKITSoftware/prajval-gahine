using ServiceStack.DataAnnotations;

namespace FirmAdvanceDemo.Enums
{
    /// <summary>
    /// Represnt current status of leave. None value is used to get all types of leaves
    /// </summary>
    [EnumAsChar]
    public enum EnmLeaveStatus
    {
        /// <summary>
        /// None
        /// </summary>
        N = 0,

        /// <summary>
        /// Pending
        /// </summary>
        P = 1,

        /// <summary>
        /// Approved
        /// </summary>
        A = 2,

        /// <summary>
        /// Rejected
        /// </summary>
        R = 3,

        /// <summary>
        /// Expired
        /// </summary>
        E = 4
    }

    /// <summary>
    /// Represnt type of punch
    /// </summary>
    [EnumAsChar]
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
    [EnumAsChar]
    public enum EnmRole : byte
    {
        /// <summary>
        /// Admin
        /// </summary>
        A = 1,

        /// <summary>
        /// Employee
        /// </summary>
        E
    }

    [EnumAsChar]
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