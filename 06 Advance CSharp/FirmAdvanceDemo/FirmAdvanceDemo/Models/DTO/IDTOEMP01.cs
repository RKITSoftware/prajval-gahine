using Newtonsoft.Json;
using System;

namespace FirmAdvanceDemo.Models.DTO
{
    internal interface IDTOEMP01
    {
        /// <summary>
        /// Employee First Name
        /// </summary>
        string P01F02 { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary
        string P01F03 { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        char P01F04 { get; set; }

        /// <summary>
        /// Employee Date of Birth
        /// </summary>
        DateTime P01F05 { get; set; }

        /// <summary>
        /// Position Id
        /// </summary>
        int P01F06 { get; set; }
    }
}
