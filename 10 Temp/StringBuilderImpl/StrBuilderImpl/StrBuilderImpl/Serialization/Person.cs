using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrBuilderImpl.Serialization
{
    [Serializable]
    internal class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Address Address { get; set; }
        public override string ToString()
        {
            return string.Format(@"Name: {0}
                Age: {1}
                Address: {2}", Name, Age, Address.ToString());
        }
    }
}
