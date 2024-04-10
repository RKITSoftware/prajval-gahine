using System;

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
