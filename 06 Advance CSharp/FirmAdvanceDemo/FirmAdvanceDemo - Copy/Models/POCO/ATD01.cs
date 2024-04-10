using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace FirmAdvanceDemo.Models.POCO
{
    [CompositeIndex(unique: true, "d01f02", "d01f03")]
    public class ATD01 : IModel
    {
        /// <summary>
        /// Attendance Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("d01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        public int d01f02 { get; set; }

        /// <summary>
        /// Date of attendance
        /// </summary>
        [ValidateNotNull]
        public DateTime d01f03 { get; set; }

        /// <summary>
        /// Day work hour
        /// </summary>
        [ValidateNotNull]
        public double d01f04 { get; set; }
    }
}