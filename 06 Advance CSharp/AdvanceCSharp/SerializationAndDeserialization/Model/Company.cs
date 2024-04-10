namespace SerializationAndDeserialization.Model
{
    /// <summary>
    /// Company model class
    /// </summary>
    [Serializable]
    public class Company
    {
        /// <summary>
        /// Company name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Catch phrase
        /// </summary>
        public string catchPhrase { get; set; }

        /// <summary>
        /// Bs
        /// </summary>
        public string bs { get; set; }
    }
}
