using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models
{
    public class PSN01
    {
        /// <summary>
        /// Position Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int n01f01 { get; set; }

        /// <summary>
        /// Position Name
        /// </summary>
        [Unique]
        [ValidateNotNull]
        public string n01f02 { get; set; }

        /// <summary>
        /// Annual Package (LPA)
        /// </summary>
        [ValidateNotNull]
        public double n01f03 { get; set; }

        /// <summary>
        /// Monthly Salary
        /// </summary>
        [ValidateNotNull]
        public double n01f04 { get; set; }

        /// <summary>
        /// Yearly Bonus
        /// </summary>
        [ValidateNotNull]
        public double n01f05 { get; set; }

        [ForeignKey(typeof(DPT01))]
        public int n01f06 { get; set; }
    }
}