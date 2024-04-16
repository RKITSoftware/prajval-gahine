using FirmAdvanceDemo.Enums;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// User-Role mapping POCO model
    /// </summary>
    public class ULE02 : IModel
    {
        /// <summary>
        /// User-Role record Id
        /// </summary>
        [PrimaryKey]
        [Alias("e02f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// User Id (Foreign key)
        /// </summary>
        public int e02f02 { get; set; }

        /// <summary>
        /// Role Id (Foreign key)
        /// </summary>
        public EnmRole e02f03 { get; set; }

        /// <summary>
        /// UserEmployee creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime e02f04 { get; set; }

        /// <summary>
        /// UserEmployee last update datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime e02f05 { get; set; }
    }
}
