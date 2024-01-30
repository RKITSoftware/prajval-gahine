﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models
{
    public class USR01EMP01
    {

        /// <summary>
        /// USR01EMP01 record Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [ForeignKey(typeof(USR01))]
        public int p01f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01))]
        public int p01f02 { get; set; }
    }
}