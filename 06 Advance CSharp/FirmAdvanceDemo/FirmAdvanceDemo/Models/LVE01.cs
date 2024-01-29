using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models
{
    public class LVE01
    {
        /// <summary>
        /// Leave Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int e01f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01))]
        public int e01f02 { get; set; }

        /// <summary>
        ///  Date of Request for Leave
        /// </summary>
        [ValidateNotNull]
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
    }
}