using FirmAdvanceDemo.BL;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Punch model representing Punch table
    /// </summary>
    public class PCH01 : IModel
    {
        /// <summary>
        /// Punch Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("d01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        public int h01f02 { get; set; }

        /// <summary>
        /// DateTime of Punch
        /// </summary>
        [ValidateNotNull]
        public DateTime h01f03 { get; set; }

        /// <summary>
        /// Punch Type
        /// </summary>
        public PunchType? h01f04 { get; set; }
    }
}