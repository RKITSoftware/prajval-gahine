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
        [AutoIncrement]
        [PrimaryKey]
        [Alias("y01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Employee Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        [JsonPropertyName("y01102")]
        public int y01f02 { get; set; }

        /// <summary>
        /// Month, Year of salary credit
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("y01103")]
        public DateTime y01f03 { get; set; }

        /// <summary>
        /// Monthly salary amount
        /// </summary>
        [ValidateNotNull]
        [JsonPropertyName("y01104")]
        public double y01f04 { get; set; }

        /// <summary>
        /// Position Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(PSN01), OnDelete = "CASCADE")]
        [JsonPropertyName("y01105")]
        public int y01f05 { get; set; }

        /// <summary>
        /// Salary creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime y01f06 { get; set; }

        /// <summary>
        /// Salary last modified datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime y01f07 { get; set; }

        public override string ToString()
        {
            return $"SalaryId: {this.Id}, EmployeeId: {this.y01f02}, SalaryDate: {this.y01f03.ToString("yyyy-MM-dd")}, WorkHour: {this.y01f04}, PositionId: {this.y01f05}";
        }
    }
}