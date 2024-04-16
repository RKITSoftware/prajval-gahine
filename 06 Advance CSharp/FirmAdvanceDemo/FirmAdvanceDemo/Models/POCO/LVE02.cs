using FirmAdvanceDemo.Enums;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Leave POCO model
    /// </summary>
    public class LVE02 : IModel
    {
        /// <summary>
        /// Leave Id
        /// </summary>
        [PrimaryKey]
        [Alias("e02f01")]
        public int e02f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        public int e02f02 { get; set; }

        /// <summary>
        ///  Date of Request for Leave
        /// </summary>
        public DateTime e02f03 { get; set; }

        /// <summary>
        /// Leave Date
        /// </summary>
        public DateTime e02f04 { get; set; }

        /// <summary>
        /// No. of leaves from Leave Date
        /// </summary>
        public int e02f05 { get; set; }

        /// <summary>
        /// Reason for leave
        /// </summary>
        public string e02f06 { get; set; }

        /// <summary>
        /// Leave status
        /// </summary>
        [IgnoreOnInsert]
        public LeaveStatus e02f07 { get; set; }

        /// <summary>
        /// Leave creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime e02f08 { get; set; }

        /// <summary>
        /// Leave last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime e02f09 { get; set; }
    }
}
