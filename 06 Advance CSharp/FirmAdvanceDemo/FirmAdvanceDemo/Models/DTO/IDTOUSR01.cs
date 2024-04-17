using FirmAdvanceDemo.Enums;
using System.Collections.Generic;

namespace FirmAdvanceDemo.Models.DTO
{
    public interface IDTOUSR01
    {
        /// <summary>
        /// username
        /// </summary>
        string r01f02 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        string r01f03 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        string r01f04 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        string r01f05 { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        List<EnmRole> r01f06 { get; set; }
    }
}
