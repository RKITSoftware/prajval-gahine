

using ServiceStack.DataAnnotations;

namespace ORMLiteDemo.Model
{
    /// <summary>
    /// User model
    /// </summary>
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
        public string r01f02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string r01f03 { get; set; }
    }
}
