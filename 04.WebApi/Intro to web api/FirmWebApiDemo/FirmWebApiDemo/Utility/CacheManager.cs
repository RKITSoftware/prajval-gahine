using System.Web.Caching;

namespace FirmWebApiDemo.Utility
{
    /// <summary>
    /// Global cache manager for application server
    /// </summary>
    public static class CacheManager
    {
        /// <summary>
        /// Global Cache instance
        /// </summary>
        public static Cache AppCache;

        /// <summary>
        /// Cache Manager static constructor to initalize global cache object
        /// </summary>
        static CacheManager()
        {
            AppCache = new Cache();
        }
    }
}