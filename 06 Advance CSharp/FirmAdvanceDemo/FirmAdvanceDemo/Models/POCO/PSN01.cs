using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace FirmAdvanceDemo.Models.POCO
{
    /// <summary>
    /// Position POCO model
    /// </summary>
    public class PSN01 : IModel
    {
        /// <summary>
        /// Position Id
        /// </summary>
        [PrimaryKey]
        [Alias("n01f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// Position Name
        /// </summary>
        public string n01f02 { get; set; }

        /// <summary>
        /// Annual Package (LPA)
        /// </summary>
        public double n01f03 { get; set; }

        /// <summary>
        /// Monthly Salary
        /// </summary>
        public double n01f04 { get; set; }

        /// <summary>
        /// Yearly Bonus
        /// </summary>
        public double n01f05 { get; set; }

        /// <summary>
        /// Department Id
        /// </summary>
        public int n01f06 { get; set; }

        /// <summary>
        /// Position creation datetime
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime n01f07 { get; set; }

        /// <summary>
        /// Position last modified datetime
        /// </summary>
        [IgnoreOnInsert]
        public DateTime n01f08 { get; set; }
    }
}
