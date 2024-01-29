using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models
{
    public class SLY01
    {
        /// <summary>
        /// Salary Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int y01f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01))]
        public int y01f02 { get; set; }

        /// <summary>
        /// Month, Year of salary credit
        /// </summary>
        [ValidateNotNull]
        public DateTime y01f03 { get; set; }

        /// <summary>
        /// Monthly salary amount
        /// </summary>
        [ValidateNotNull]
        public double y01f04 { get; set; }

        /// <summary>
        /// Position Id
        /// </summary>
        [ForeignKey(typeof(PSN01))]
        public int y01f05 { get; set; }
    }
}