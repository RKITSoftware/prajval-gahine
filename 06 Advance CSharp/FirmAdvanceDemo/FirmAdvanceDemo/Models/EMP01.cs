using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models
{
    public class EMP01
    {
        [AutoIncrement]
        [PrimaryKey]
        public int p01f01 { get; set; }

        [ValidateNotNull]
        public string p01f02 { get; set; }

        [ValidateNotNull]
        public string p01f03 { get; set; }

        [ValidateNotNull]
        public char p01f04 { get; set; }

        [ValidateNotNull]
        public DateTime p01f05 { get; set; }

        [ForeignKey(typeof(PSN01))]
        public int p01f06 { get; set; }
    }
}