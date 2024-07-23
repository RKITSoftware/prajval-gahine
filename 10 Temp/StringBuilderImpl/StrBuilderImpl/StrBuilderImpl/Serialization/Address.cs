using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrBuilderImpl.Serialization
{
    [Serializable]
    internal class Address
    {
        public int PlotNo { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public override string ToString()
        {
            return string.Format(@"PlotNo: {0}
                Street: {1}
                City: {2}
                State: {3}", PlotNo, Street, City, State);
        }
    }
}
