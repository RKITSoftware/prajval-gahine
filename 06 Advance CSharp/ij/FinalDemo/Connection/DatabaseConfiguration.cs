namespace FinalDemo.Connection
{
    /// <summary>
    /// Represents the configuration settings for connecting to a database.
    /// </summary>
    public class DatabaseConfiguration
    {
        /// <summary>
        /// Gets or sets the port number for the database server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the name of the database server.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the name of the database to connect to.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets the username for authenticating with the database server.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the password for authenticating with the database server.
        /// </summary>
        public string Password { get; set; }
    }
}