using ServiceStack.DataAnnotations;

namespace LINQDemo.Model
{
    /// <summary>
    /// Model class to model Attendance
    /// </summary>
    [Alias("atd01")]
    internal class ATD01
    {
        /// <summary>
        /// Attendance Id
        /// </summary>
        public int d01f01 { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        public int d01f02 { get; set; }

        /// <summary>
        /// Attendance date
        /// </summary>
        public DateOnly d01f03 { get; set; } = new DateOnly();


        /// <summary>
        /// day work hour
        /// </summary>
        public double d01f04 { get; set; }
    }
}
