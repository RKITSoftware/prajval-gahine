namespace SerializationAndDeserialization.Model
{
    /// <summary>
    /// Address model class
    /// </summary>
    [Serializable]
    public class Address
    {
        /// <summary>
        /// Street
        /// </summary>
        public string street { get; set; }

        /// <summary>
        /// Suite
        /// </summary>
        public string suite { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// Zipcode
        /// </summary>
        public string zipcode { get; set; }

        /// <summary>
        /// Geo
        /// </summary>
        public Geo geo { get; set; }
    }
}
