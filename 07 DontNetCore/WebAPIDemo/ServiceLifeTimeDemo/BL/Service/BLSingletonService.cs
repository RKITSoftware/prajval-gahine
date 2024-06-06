using ServiceLifeTimeDemo.BL.Interface;

namespace ServiceLifeTimeDemo.BL.Service
{
    /// <summary>
    /// BLSingletonService class
    /// </summary>
    public class BLSingletonService : ISingletonService
    {
        /// <summary>
        /// Guid instance
        /// </summary>
        public Guid GUID { get; set; }

        /// <summary>
        /// BLSingletonService constructor
        /// </summary>
        public BLSingletonService()
        {
            GUID = Guid.NewGuid();
        }
    }
}
