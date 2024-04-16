using ServiceStack;
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
        [Alias("p02f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// User Id (Foreign key)
        /// </summary>
        public int p02f02 { get; set; }

        /// <summary>
        /// Employee Id (Foreign key)
        /// </summary>
        public int p02f03 { get; set; }

        /// <summary>
        /// UserEmployee creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime p02f04 { get; set; }

        /// <summary>
        /// UserEmployee last update datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime p02f05 { get; set; }
    }
}
