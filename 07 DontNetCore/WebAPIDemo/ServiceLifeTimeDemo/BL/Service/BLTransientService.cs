using ServiceLifeTimeDemo.BL.Interface;

namespace ServiceLifeTimeDemo.BL.Service
{
    public class BLTransientService : ITransientService
    {
        public Guid GUID { get; set; }

        public BLTransientService()
        {
            GUID = Guid.NewGuid();
        }
    }
}
