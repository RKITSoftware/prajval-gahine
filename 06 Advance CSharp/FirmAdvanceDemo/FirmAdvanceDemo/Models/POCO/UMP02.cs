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
        [AutoIncrement]
        [PrimaryKey]
        [Alias("p02f01")]
        public int Id { get; set; }

        /// <summary>
        /// User Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(USR01), OnDelete = "CASCADE")]
        public int p02f02 { get; set; }

        /// <summary>
        /// Employee Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        public int p02f03 { get; set; }

        /// <summary>
        /// UserEmployee creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime p02f04 { get; set; }

        /// <summary>
        /// UserEmployee last update datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime p02f05 { get; set; }
    }
}