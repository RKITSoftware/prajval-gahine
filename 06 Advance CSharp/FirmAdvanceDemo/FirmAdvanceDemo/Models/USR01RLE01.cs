using ServiceStack.DataAnnotations;

namespace FirmAdvanceDemo.Models
{
    public class USR01RLE01 : IModel
    {
        /// <summary>
        /// USR01RLE01 record Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// User Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(USR01), OnDelete = "CASCADE")]
        public int r01f01 { get; set; }

        /// <summary>
        /// Role Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(RLE01), OnDelete = "CASCADE")]
        public int r01f02 { get; set; }
    }
}