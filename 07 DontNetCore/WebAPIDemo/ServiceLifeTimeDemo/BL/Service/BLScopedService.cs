using ServiceLifeTimeDemo.BL.Interface;

namespace ServiceLifeTimeDemo.BL.Service
{
    /// <summary>
    /// BLScopedService class
    /// </summary>
    public class BLScopedService : IScopedService
    {
        /// <summary>
        /// Guid instance
        /// </summary>
        public Guid GUID { get; set; }

        /// <summary>
        /// BLScopedService constructor
        /// </summary>
        public BLScopedService()
        {
            GUID = Guid.NewGuid();
        }
    }
}
