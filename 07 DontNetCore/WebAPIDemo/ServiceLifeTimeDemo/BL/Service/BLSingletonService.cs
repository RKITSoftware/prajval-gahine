using ServiceLifeTimeDemo.BL.Interface;

namespace ServiceLifeTimeDemo.BL.Service
{
    public class BLSingletonService : ISingletonService
    {
        public Guid GUID { get; set; }

        public BLSingletonService()
        {
            GUID = Guid.NewGuid();
        }
    }
}
