using System;

namespace FirmAdvanceDemo.Models.DTO
{
    internal interface IDTOEMP01
    {
        /// <summary>
        /// Employee First Name
        /// </summary>
        string p01f02 { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary
        string p01f03 { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        char p01f04 { get; set; }

        /// <summary>
        /// Employee Date of Birth
        /// </summary>
        DateTime p01f05 { get; set; }

        /// <summary>
        /// Position Id
        /// </summary>
        int p01f06 { get; set; }
    }
}
