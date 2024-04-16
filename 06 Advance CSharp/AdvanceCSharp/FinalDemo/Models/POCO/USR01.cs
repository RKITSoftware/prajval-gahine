using System;

namespace DatabaseWithCrudWebApi.Models
{
    /// <summary>
    /// User POCO model
    /// </summary>
    public class USR01
    {
        #region Property

        /// <summary>
        /// User Id
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Creation date time
        /// </summary>
        public DateTime R01F04 { get; set; }


        /// <summary>
        /// Last modified date time
        /// </summary>
        public DateTime R01F05 { get; set; }

        #endregion
    }
}