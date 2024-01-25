using ServiceStack.DataAnnotations;

namespace FinalDemo.Models
{
    /// <summary>
    /// ApplicationTable
    /// </summary>
    public class app01
    {
        /// <summary>
        /// Application ID
        /// </summary>
        [AutoIncrement]
        public int p01f01 { get; set; }
        /// <summary>
        /// JobID
        /// </summary>
        public int p01f02 { get; set; }
        /// <summary>
        /// CadidateName
        /// </summary>
        public string p01f03 { get; set; }
        /// <summary>
        /// Resume
        /// </summary>
        public string p01f04 { get; set; }
        /// <summary>
        /// Coverletter
        /// </summary>
        public string p01f05 { get; set; }
    }
}