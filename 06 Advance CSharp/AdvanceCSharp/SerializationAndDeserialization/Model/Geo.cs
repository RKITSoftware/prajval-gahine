using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationAndDeserialization.Model
{
    [Serializable]
    public class Geo
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }
}
