using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Attendance POCO model
    /// </summary>
    [CompositeIndex(unique: true, "d01f02", "d01f03")]
    public class ATD01 : IModel
    {
        /// <summary>
        /// Attendance Id
        /// </summary>
        [PrimaryKey]
        [Alias("d01f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        public int d01f02 { get; set; }

        /// <summary>
        /// Date of attendance
        /// </summary>
        public DateTime d01f03 { get; set; }

        /// <summary>
        /// Day work hour
        /// </summary>
        public double d01f04 { get; set; }

        /// <summary>
        /// Attendance creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime d01f05 { get; set; }

        /// <summary>
        /// Attendance last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime d01f06 { get; set; }
    }
}
