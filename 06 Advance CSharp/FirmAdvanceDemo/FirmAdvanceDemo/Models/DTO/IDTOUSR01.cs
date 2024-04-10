using FirmAdvanceDemo.Enums;
using System.Collections.Generic;

namespace FirmAdvanceDemo.Models.DTO
{
    public interface IDTOUSR01
    {        
        /// <summary>
        /// username
        /// </summary>
        string r01102 { get; set; }

        /// <summary>
        /// user hashed password
        /// </summary>
        string r01103 { get; set; }

        /// <summary>
        /// user emailId
        /// </summary>
        string r01104 { get; set; }

        /// <summary>
        /// User phone no.
        /// </summary>
        string r01105 { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        List<EnmRole> r01106 { get; set; }
    }
}
