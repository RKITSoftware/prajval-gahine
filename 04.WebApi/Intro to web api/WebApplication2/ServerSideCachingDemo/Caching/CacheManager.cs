using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace ServerSideCachingDemo.Caching
{
    public class CacheManager
    {
        public static Cache CacheSSCD;
        static CacheManager()
        {
            CacheSSCD = new Cache();
        }
    }
}