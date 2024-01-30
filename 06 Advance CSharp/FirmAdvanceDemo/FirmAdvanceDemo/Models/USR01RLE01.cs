using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models
{
    public class USR01RLE01 : IModel
    {
        /// <summary>
        /// USR01RLE01 record Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

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