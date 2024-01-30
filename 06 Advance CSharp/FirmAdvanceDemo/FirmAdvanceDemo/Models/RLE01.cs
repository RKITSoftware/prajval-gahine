using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models
{
    /// <summary>
    /// Role class
    /// </summary>
    public class RLE01 : IModel
    {

        public static string TableName;
        static RLE01()
        {
            TableName = "Role";
        }


        /// <summary>
        /// Role Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("e01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [ValidateNotNull]
        public string e01f02 { get; set; }
    }
}