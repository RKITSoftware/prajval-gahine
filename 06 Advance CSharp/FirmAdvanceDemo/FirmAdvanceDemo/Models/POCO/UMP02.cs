using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// User-Employee mapping POCO model
    /// </summary>
    public class UMP02
    {

        /// <summary>
        /// UMP02 record Id
        /// </summary>
        [PrimaryKey]
        public int P02F01 { get; set; }

        /// <summary>
        /// User Id (Foreign key)
        /// </summary>
        public int P02F02 { get; set; }

        /// <summary>
        /// Employee Id (Foreign key)
        /// </summary>
        public int P02F03 { get; set; }

        /// <summary>
        /// UserEmployee creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime P02F04 { get; set; }

        /// <summary>
        /// UserEmployee last update datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? P02F05 { get; set; }
    }
}
