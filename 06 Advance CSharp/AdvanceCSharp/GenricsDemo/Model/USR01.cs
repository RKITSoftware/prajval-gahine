namespace GenricsDemo.Model
{
    /// <summary>
    /// User model class
    /// </summary>
    internal class USR01 : IResource
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string? r01f02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string? r01f03 { get; set; }

        /// <summary>
        /// User Roles
        /// </summary>
        public string? r01f04 { get; set; }

        /// <summary>
        /// User model class constructor
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="roles">Roles</param>
        public USR01(int id, string username, string password, string roles)
        {
            this.id = id;
            this.r01f02 = username;
            this.r01f03 = password;
            this.r01f04 = roles;
        }
    }
}
