using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models
{
    public class RLE01USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [ForeignKey(typeof(USR01))]
        public int r01f01 { get; set; }

        /// <summary>
        /// Role Id
        /// </summary>
        [ForeignKey(typeof(RLE01))]
        public int r01f02 { get; set; }
    }
}