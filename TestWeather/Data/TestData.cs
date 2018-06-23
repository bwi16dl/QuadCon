using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [DataContract]
    public class TestData
    {
        [DataMember]
        public double Temperature;
        [DataMember]
        public string Wind;
    }
}
