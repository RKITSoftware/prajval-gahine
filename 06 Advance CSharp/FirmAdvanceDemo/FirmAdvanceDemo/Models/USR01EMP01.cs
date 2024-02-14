using ServiceStack.DataAnnotations;

namespace FirmAdvanceDemo.Models
{
    public class USR01EMP01
    {

        /// <summary>
        /// USR01EMP01 record Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// User Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(USR01), OnDelete = "CASCADE")]
        public int p01f01 { get; set; }

        /// <summary>
        /// Employee Id (Foreign key)
        /// </summary>
        [ForeignKey(typeof(EMP01), OnDelete = "CASCADE")]
        public int p01f02 { get; set; }
    }
}