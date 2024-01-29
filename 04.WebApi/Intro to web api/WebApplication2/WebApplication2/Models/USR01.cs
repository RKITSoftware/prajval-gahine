
namespace WebApplication2.Models
{
    /// <summary>
    /// User class to store user specific data
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User id
        /// </summary>
        public int r01f01 { get; set; }

        /// <summary>
        /// Username of the user
        /// </summary>
        public string r01f02 { get; set; }

        /// <summary>
        /// password of user for authentication purpose
        /// </summary>
        public string r01f03 { get; set; }

        /// <summary>
        /// user role for authorization purpose
        /// </summary>
        public string r01f04 { get; set; }
    }
}