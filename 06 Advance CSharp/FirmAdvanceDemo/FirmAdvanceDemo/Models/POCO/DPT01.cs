using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Department POCO model
    /// </summary>
    public class DPT01 : IModel
    {
        /// <summary>
        /// Department Id
        /// </summary>
        [PrimaryKey]
        [Alias("t01f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        public string t01f02 { get; set; }

        /// <summary>
        /// Department creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime t01f03 { get; set; }

        /// <summary>
        /// Department last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime t01f04 { get; set; }
    }
}
