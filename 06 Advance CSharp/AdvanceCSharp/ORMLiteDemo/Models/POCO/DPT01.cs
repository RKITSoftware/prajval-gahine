using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Department POCO model
    /// </summary>
    public class DPT01
    {
        /// <summary>
        /// Department Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int T01F01 { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        public string T01F02 { get; set; }

        /// <summary>
        /// Department creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime T01F03 { get; set; }

        /// <summary>
        /// Department last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime? T01F04 { get; set; }
    }
}
