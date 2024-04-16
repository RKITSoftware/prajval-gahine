using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Employee POCO model
    /// </summary>
    public class EMP01 : IModel
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        [PrimaryKey]
        [Alias("p01f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// Employee First Name
        /// </summary>
        public string p01f02 { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary>
        public string p01f03 { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        public char p01f04 { get; set; }

        /// <summary>
        /// Employee Date of Birth
        /// </summary>
        public DateTime p01f05 { get; set; }

        /// <summary>
        /// Position Id (Foreign key)
        /// </summary>
        public int p01f06 { get; set; }

        /// <summary>
        /// Employee creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime p01f07 { get; set; }

        /// <summary>
        /// Employee last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime p01f08 { get; set; }
    }
}
