﻿using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models
{
    public class ATD01
    {
        /// <summary>
        /// Attendance Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int d01f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01))]
        public int d01f02 { get; set; }

        /// <summary>
        /// Date of attendance
        /// </summary>
        [ValidateNotNull]
        public DateTime d01f03 { get; set; }

        /// <summary>
        /// Day work hour
        /// </summary>
        [ValidateNotNull]
        public double d01f04 { get; set; }
    }
}