using ORMLiteDemo.Enums;
using ServiceStack.DataAnnotations;
using System;

namespace ORMLiteDemo.Models.POCO
{
    /// <summary>
    /// Role POCO model
    /// </summary>
    public class RLE01
    {
        /// <summary>
        /// Role Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
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
}


//@"CREATE TABLE `PCH01` \n(\n  `P01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, \n  `H01F02` INT(11) NOT NULL, \n  `H01F03` CHAR(1) NOT NULL DEFAULT U, \n  `H01F04` DATETIME NOT NULL, \n  `H01F05` DATETIME NULL \n); \n"