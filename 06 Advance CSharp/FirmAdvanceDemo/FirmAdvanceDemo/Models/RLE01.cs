using ServiceStack;
using ServiceStack.DataAnnotations;

namespace FirmAdvanceDemo.Models
{
    /// <summary>
    /// Role class
    /// </summary>
    public class RLE01 : IModel
    {
        /// <summary>
        /// Role Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("e01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [ValidateNotNull]
        public string e01f02 { get; set; }
    }
}