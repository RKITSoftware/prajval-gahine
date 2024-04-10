using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseWithCrudWebApi.Models
{
    /// <summary>
    /// User model
    /// </summary>
    internal class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int r01f01 { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [JsonProperty("r01102")]
        public string r01f02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        [JsonProperty("r01103")]
        public string r01f03 { get; set; }

        /// <summary>
        /// Creation date time
        /// </summary>
        public DateTime r01f04 { get; set; }


        /// <summary>
        /// Last modified date time
        /// </summary>
        public DateTime r01f05 { get; set; }
    }
}