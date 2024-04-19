using FirmAdvanceDemo.Enums;
using System.Collections.Generic;

namespace FirmAdvanceDemo.Models.DTO
{
    public interface IDTOUSR01
    {
        /// <summary>
        /// username
        /// </summary>
        string R01F02 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        string R01F03 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        string R01F04 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        string R01F05 { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        List<EnmRole> R01F06 { get; set; }
    }
}
