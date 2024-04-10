using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using FirmAdvanceDemo.Enums;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// User-Role mapping POCO model
    /// </summary>
    public class ULE02 : IModel
    {
        /// <summary>
        /// USR01RLE01 record Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("e02f01")]
        public int Id { get; set; }

        /// <summary>
        /// User Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(USR01), OnDelete = "CASCADE")]
        public int e02f02 { get; set; }

        /// <summary>
        /// Role Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(RLE01), OnDelete = "CASCADE")]
        public EnmRole e02f03 { get; set; }

        /// <summary>
        /// UserEmployee creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime e02f04 { get; set; }

        /// <summary>
        /// UserEmployee last update datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime e02f05 { get; set; }
    }
}