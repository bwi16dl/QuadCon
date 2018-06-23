using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DataCollector
{
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
