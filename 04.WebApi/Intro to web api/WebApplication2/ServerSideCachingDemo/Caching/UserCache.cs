using ServerSideCachingDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerSideCachingDemo.Caching
{
    public class UserCache
    {
        public int SessionId;

        public USR01 user;

        public UserCache(int sessionId, USR01 user)
        {
            this.SessionId = sessionId;
            this.user = user;
        }
    }
}