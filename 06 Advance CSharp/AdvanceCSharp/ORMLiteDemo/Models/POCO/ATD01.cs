using ServiceStack.DataAnnotations;
using System;

namespace ORMLiteDemo.Models.POCO
{
    /// <summary>
    /// Attendance POCO model
    /// </summary>
    public class ATD01
    {
        /// <summary>
        /// Attendance Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int D01F01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        public int D01F02 { get; set; }

        /// <summary>
        /// Date of attendance
        /// </summary>
        public DateTime D01F03 { get; set; }

        /// <summary>
        /// Day work hour
        /// </summary>
        public double D01F04 { get; set; }

        /// <summary>
        /// Attendance creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime D01F05 { get; set; }

        /// <summary>
        /// Attendance last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? D01F06 { get; set; }
    }
}
