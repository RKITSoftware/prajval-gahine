using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FirmAdvanceDemo.Models.DTO
{
    internal interface IDTOEMP01
    {
        /// <summary>
        /// Employee First Name
        /// </summary>
        string p01102 { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary
        string p01103 { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        char p01104 { get; set; }

        /// <summary>
        /// Employee Date of Birth
        /// </summary>
        DateTime p01105 { get; set; }

        /// <summary>
        /// Position Id
        /// </summary>
        int p01106 { get; set; }
    }
}
