using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DataCollector
{
    // Data object that represents a source, its attributes and methods exposed for admin purposes
    // Everything is implemented as list of strings due to reflections (type safety is provided on client side, by exposing the queried attributes and methods in dropdown boxes to choose from)
    [DataContract]
    public class ExposedData
    {
        #region INTERFACE
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public List<string> Attributes { get; set; }
        [DataMember]
        public List<string> Actions { get; set; }
        #endregion

        public ExposedData(string source, List<string> attributes, List<string> actions)
        {
            Source = source;
            Attributes = attributes;
            Actions = actions;
        }
    }
}
