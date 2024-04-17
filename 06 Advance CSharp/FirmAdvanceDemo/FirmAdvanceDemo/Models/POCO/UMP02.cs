using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// User-Employee mapping POCO model
    /// </summary>
    public class UMP02 : IModel
    {

        /// <summary>
        /// USR01EMP01 record Id
        /// </summary>
        [PrimaryKey]
        public int P01F01 { get; set; }

        /// <summary>
        /// User Id (Foreign key)
        /// </summary>
        public int P01F02 { get; set; }

        /// <summary>
        /// Employee Id (Foreign key)
        /// </summary>
        public int P01F03 { get; set; }

        /// <summary>
        /// UserEmployee creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime P01F04 { get; set; }

        /// <summary>
        /// UserEmployee last update datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime P01F05 { get; set; }
    }
}
