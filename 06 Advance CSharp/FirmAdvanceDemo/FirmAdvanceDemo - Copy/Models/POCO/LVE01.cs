using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirmAdvanceDemo.Models.POCO
{
    [CompositeIndex(true, "e01f02", "e01f04")]
    public class LVE01 : IModel
    {
        /// <summary>
        /// Leave Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("e01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        public int e01f02 { get; set; }

        /// <summary>
        ///  Date of Request for Leave
        /// </summary>
        [ValidateNotNull]
        [TypeConverter("Date")]
        public DateTime e01f03 { get; set; }

        /// <summary>
        /// Leave Date
        /// </summary>
        [ValidateNotNull]
        public DateTime e01f04 { get; set; }

        /// <summary>
        /// No. of leaves from Leave Date
        /// </summary>
        [ValidateNotNull]
        public int e01f05 { get; set; }

        /// <summary>
        /// Reason for leave
        /// </summary>
        [ValidateNotNull]
        public string e01f06 { get; set; }

        /// <summary>
        /// Leave status
        /// </summary>
        [ValidateNotNull]
        [DataType("int")]
        public LeaveStatus e01f07 { get; set; }
    }

    /// <summary>
    /// Represnt current status of leave. None value is used to get all types of leaves
    /// </summary>
    [EnumAsInt]
    public enum LeaveStatus
    {
        None = 0,
        Pending,
        Approved,
        Rejected,
        Expired
    }
}