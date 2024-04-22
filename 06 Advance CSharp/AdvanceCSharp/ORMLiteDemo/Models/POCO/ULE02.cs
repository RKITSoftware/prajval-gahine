using ORMLiteDemo.Enums;
using ServiceStack.DataAnnotations;
using System;

namespace ORMLiteDemo.Models.POCO
{
    /// <summary>
    /// User-Role mapping POCO model
    /// </summary>
    public class ULE02
    {
        /// <summary>
        /// User-Role record Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int E02F01 { get; set; }

        /// <summary>
        /// User Id (Foreign key)
        /// </summary>
        public int E02F02 { get; set; }

        /// <summary>
        /// Role Id (Foreign key)
        /// </summary>
        public int E02F03 { get; set; }

        /// <summary>
        /// UserEmployee creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime E02F04 { get; set; }

        /// <summary>
        /// UserEmployee last update datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? E02F05 { get; set; }
    }
}