using ServiceLifeTimeDemo.BL.Interface;

namespace ServiceLifeTimeDemo.BL.Service
{
    /// <summary>
    /// BLTransientService class
    /// </summary>
    public class BLTransientService : ITransientService
    {
        /// <summary>
        /// Guid instance
        /// </summary>
        public Guid GUID { get; set; }

        /// <summary>
        /// BLTransientService constructor
        /// </summary>
        public BLTransientService()
        {
            GUID = Guid.NewGuid();
        }
    }
}
