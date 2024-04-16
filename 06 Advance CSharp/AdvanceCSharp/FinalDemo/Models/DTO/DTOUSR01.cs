using Newtonsoft.Json;

namespace DatabaseWithCrudWebApi.Models
{
    /// <summary>
    /// User DTO model
    /// </summary>
    public class DTOUSR01
    {
        #region Public Properties

        /// <summary>
        /// Userid
        /// </summary>
        [JsonProperty("r01101")]
        public int R01F01 { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [JsonProperty("r01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        [JsonProperty("r01103")]
        public string R01F03 { get; set; }
    
        #endregion
    }
}