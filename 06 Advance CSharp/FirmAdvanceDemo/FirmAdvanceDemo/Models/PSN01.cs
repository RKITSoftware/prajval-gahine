using ServiceStack;
using ServiceStack.DataAnnotations;

namespace FirmAdvanceDemo.Models
{
    public class PSN01 : IModel
    {
        /// <summary>
        /// Position Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("n01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Position Name
        /// </summary>
        [Unique]
        [ValidateNotNull]
        public string n01f02 { get; set; }

        /// <summary>
        /// Annual Package (LPA)
        /// </summary>
        [ValidateNotNull]
        public double n01f03 { get; set; }

        /// <summary>
        /// Monthly Salary
        /// </summary>
        [ValidateNotNull]
        public double n01f04 { get; set; }

        /// <summary>
        /// Yearly Bonus
        /// </summary>
        [ValidateNotNull]
        public double n01f05 { get; set; }

        /// <summary>
        /// Department Id
        /// </summary>
        [ForeignKey(typeof(DPT01), OnDelete = "CASCADE")]
        public int n01f06 { get; set; }
    }
}