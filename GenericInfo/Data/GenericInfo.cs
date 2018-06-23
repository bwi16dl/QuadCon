using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GenericInfo.Data
{
    [DataContract]
    public class GenericInfo
    {
        [DataMember]
        public string SourceName { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string SourceError { get; set; }

        public GenericInfo()
        {

        }
    }
}
