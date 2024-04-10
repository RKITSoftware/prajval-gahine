namespace SerializationAndDeserialization.Model
{
    /// <summary>
    /// Geo model class
    /// </summary>
    [Serializable]
    public class Geo
    {
        /// <summary>
        /// Latitude
        /// </summary>
        public string lat { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public string lng { get; set; }
    }
}
