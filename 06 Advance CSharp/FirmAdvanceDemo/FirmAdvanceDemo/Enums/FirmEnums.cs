using ServiceStack.DataAnnotations;

namespace FirmAdvanceDemo.Enums
{
    /// <summary>
    /// Represnt current status of leave. None value is used to get all types of leaves
    /// </summary>
    [EnumAsInt]
    public enum LeaveStatus
    {
        None = 0,
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Expired = 4
    }

    /// <summary>
    /// Represnt type of punch
    /// </summary>
    [EnumAsInt]
    public enum EnmPunchType : byte
    {
        In,
        Out,
        Mistaken,
        Ambiguous,
        VirtualIn,
        VirtualOut,
    }

    /// <summary>
    /// Represent type of operation to be performed
    /// </summary>
    public enum EnmDBOperation : byte
    {
        Create,
        Update
    }

    /// <summary>
    /// Represent type of role of user
    /// </summary>
    [EnumAsInt]
    public enum EnmRole : byte
    {
        Admin = 1,
        Employee
    }
}