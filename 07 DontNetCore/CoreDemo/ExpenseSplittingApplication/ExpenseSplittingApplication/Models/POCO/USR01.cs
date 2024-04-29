﻿using ServiceStack.DataAnnotations;

namespace ExpenseSplittingApplication.Models.POCO
{
    public class USR01
    {
        /// <summary>
        /// User ID
        /// </summary>
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// User creation datetime
        /// </summary>
        public DateTime R01F098 { get; set; }

        /// <summary>
        /// User last modified datetime
        /// </summary>
        public DateTime? R01F099 { get; set; }
    }
}
