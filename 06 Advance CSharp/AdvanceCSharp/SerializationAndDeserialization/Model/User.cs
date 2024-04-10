namespace SerializationAndDeserialization.Model
{
    /// <summary>
    /// User model class
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// User id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public Address address { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// Website link
        /// </summary>
        public string website { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public Company company { get; set; }
    }
}
