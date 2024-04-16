using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Salary POCO model
    /// </summary>
    public class SLY01 : IModel
    {
        /// <summary>
        /// Salary Id
        /// </summary>
        [PrimaryKey]
        [Alias("y01f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// Employee Id (Foreign key)
        /// </summary>
        public int y01f02 { get; set; }

        /// <summary>
        /// Month, Year of salary credit
        /// </summary>
        public DateTime y01f03 { get; set; }

        /// <summary>
        /// Monthly salary amount
        /// </summary>
        public double y01f04 { get; set; }

        /// <summary>
        /// Position Id (Foreign key)
        /// </summary>
        public int y01f05 { get; set; }

        /// <summary>
        /// Salary creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime y01f06 { get; set; }

        /// <summary>
        /// Salary last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime y01f07 { get; set; }
    }
}
