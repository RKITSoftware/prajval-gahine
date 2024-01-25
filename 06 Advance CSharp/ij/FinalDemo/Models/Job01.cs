using ServiceStack.DataAnnotations;

namespace FinalDemo.Models
{
    public class Job01
    {
        /// <summary>
        /// JobID
        /// </summary>
        [AutoIncrement]
        public int b01f01 { get; set; }
        /// <summary>
        /// JobTitle
        /// </summary>
        public string b01f02 { get; set; }
        /// <summary>
        /// Job Discription
        /// </summary>
        public string b01f03 { get; set; }
    }
}