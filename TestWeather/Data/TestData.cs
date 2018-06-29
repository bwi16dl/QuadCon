using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    // A small object to demonstrate how client can work with OOP
    // A forecast consists of one or three TestData objects, each one having temperature and wind
    [DataContract]
    public class TestData
    {
        [DataMember]
        public double Temperature;
        [DataMember]
        public string Wind;
    }
}
