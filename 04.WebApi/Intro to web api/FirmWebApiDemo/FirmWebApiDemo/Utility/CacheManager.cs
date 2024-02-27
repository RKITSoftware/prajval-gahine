using System.Web.Caching;

namespace FirmWebApiDemo.Utility
{
    public static class CacheManager
    {
        public static Cache AppCache;

        static CacheManager()
        {
            AppCache = new Cache();
        }
    }
}