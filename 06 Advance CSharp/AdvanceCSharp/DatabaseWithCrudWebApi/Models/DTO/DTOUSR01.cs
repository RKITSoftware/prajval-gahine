using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseWithCrudWebApi.Models
{
    public class DTOUSR01
    {
        /// <summary>
        /// Username
        /// </summary>
        [JsonProperty("r01f02")]
        public string r01102 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        [JsonProperty("r01f03")]
        public string r01103 { get; set; }
    }
}