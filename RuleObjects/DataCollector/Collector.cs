using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessRules.DataCollector
{
    // A class that collects all exposed information from loaded sources
    // It is part of data contract, since everything is transferred to client as one big object
    [DataContract]
    public static class Collector
    {
        [DataMember]
        public static List<ExposedData> ExposedData { get; set; }
        public static List<ExposedData> GetExposedData() { return ExposedData; }

        #region INTERNAL
        // Main logic to loop through all sources
        public static void Collect()
        {
            ExposedData = new List<ExposedData>();

            foreach (var i in TestObject.Connectors) { ExposedData.Add(CreateConstructor(i)); }
            foreach (var i in KodiObject.Connectors) { ExposedData.Add(CreateConstructor(i)); }
            foreach (var i in GenericInfoObject.Connectors) { ExposedData.Add(CreateConstructor(i)); }
            foreach (var i in WeatherObject.Connectors) { ExposedData.Add(CreateConstructor(i)); }
        }

        // Logic to process each source and collect exposed attributes and methods
        private static ExposedData CreateConstructor(Object from)
        {
            string source = from.GetType().GetMethod("GetName").Invoke(from, null).ToString();
            List<string> attributes = new List<string>();
            List<string> actions = new List<string>();

            foreach (var j in from.GetType().GetMethods())
            {
                if (Regex.IsMatch(j.Name, "^AGet.*")) { attributes.Add(j.Name.Remove(0, 4)); }
                else if (Regex.IsMatch(j.Name, "^A.*")) { actions.Add(j.Name.Remove(0, 1)); }
            }
            
            return new ExposedData(source, attributes, actions);
        }
        #endregion
    }
}
