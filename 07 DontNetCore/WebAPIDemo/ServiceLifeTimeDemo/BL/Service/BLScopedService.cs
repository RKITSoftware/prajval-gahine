using ServiceLifeTimeDemo.BL.Interface;

namespace ServiceLifeTimeDemo.BL.Service
{
    public class BLScopedService : IScopedService
    {
        public Guid GUID { get; set; }

        public BLScopedService()
        {
            GUID = Guid.NewGuid();
        }
    }
}
