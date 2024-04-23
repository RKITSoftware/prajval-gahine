using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace ORMLiteDemo.Models.POCO
{
    /// <summary>
    /// Department POCO model
    /// </summary>
    public class DPT01
    {
        /// <summary>
        /// Department Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int T01F01 { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [ValidateNotNull]
        public string T01F02 { get; set; }

        /// <summary>
        /// Department creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        [ValidateNotNull]
        public DateTime T01F03 { get; set; }

        /// <summary>
        /// Department last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? T01F04 { get; set; }
    }
}
