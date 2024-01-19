using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenricsDemo.Model
{
    internal class USR01 : IResource
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string? r01f02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string? r01f03 { get; set; }

        /// <summary>
        /// User Roles
        /// </summary>
        public string? r01f04 { get; set; }

        public USR01(int id, string username, string password, string roles)
        {
            this.id = id;
            this.r01f02 = username;
            this.r01f03 = password;
            this.r01f04 = roles;
        }
    }
}
