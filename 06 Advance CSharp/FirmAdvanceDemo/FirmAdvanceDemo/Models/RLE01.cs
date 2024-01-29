using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models
{
    /// <summary>
    /// Role class
    /// </summary>
    public class RLE01
    {
        /// <summary>
        /// Role Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int e01f01 { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [ValidateNotNull]
        public string e01f02 { get; set; }
    }
}