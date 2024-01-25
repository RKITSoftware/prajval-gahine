

using ServiceStack.DataAnnotations;

namespace ORMLiteDemo.Model
{
    internal class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int r01f01 { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [Index(true)]
        public string r01f02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string r01f03 { get; set; }
    }
}
