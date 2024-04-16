using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Role POCO model
    /// </summary>
    public class RLE01 : IModel
    {
        /// <summary>
        /// Role Id
        /// </summary>
        [PrimaryKey]
        [Alias("e01f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string e01f02 { get; set; }

        /// <summary>
        ///  creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime e01f03 { get; set; }

        /// <summary>
        ///  last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime e01f04 { get; set; }
    }
}
