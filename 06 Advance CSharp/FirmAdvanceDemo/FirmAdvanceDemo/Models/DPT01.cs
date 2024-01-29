using ServiceStack;
using ServiceStack.DataAnnotations;

namespace FirmAdvanceDemo.Models
{
    public class DPT01
    {
        /// <summary>
        /// Department Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int t01f01 { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [ValidateNotNull]
        public string t01f02 { get; set; }
    }
}