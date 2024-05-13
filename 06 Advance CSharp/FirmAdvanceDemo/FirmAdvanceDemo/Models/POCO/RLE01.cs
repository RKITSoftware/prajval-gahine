using FirmAdvanceDemo.Enums;
using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Role POCO model
    /// </summary>
    public class RLE01
    {
        #region Public Properties
        /// <summary>
        /// Role Id
        /// </summary>
        [PrimaryKey]
        public int E01F01 { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public EnmRole E01F02 { get; set; }

        /// <summary>
        ///  creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime E01F03 { get; set; }

        /// <summary>
        ///  last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? E01F04 { get; set; }
    }
    #endregion
}
