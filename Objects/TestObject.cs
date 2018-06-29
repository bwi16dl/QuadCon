using GenericInfo;
using Kodi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;
using Weather;


// NOTE: all classes in Objects namespace have identical structure and functionality, hence detailed comments are only put here
namespace Objects
{
    // Static class to keep a list of connectors (aka .dll files), should there be more than one of them
    // For example, this list contains Test weather source 01 and Test weather source 2
    // Note that unique ID of a connector is a combination of its type (in this case ITest) and name
    public static class TestObject
    {
        // A list of connectors, which implement ITest interface
        public static List<ITest> Connectors = new List<ITest>();

        // Helper method to check if the source is present in connectors (by name)
        public static bool Exists(string name)
        {
            foreach (var i in Connectors) { if (i.GetName().Equals(name)) { return true; } }
            return false;   
        }

        // Helper method to return relevant source (by name)
        // Note that it is heavily used by reflection logic and web services
        public static ITest Find(string name)
        {
            foreach (var i in Connectors) { if (i.GetName().Equals(name)) { return i; } }
            return null;
        }

        // Helper method to add a new connector of type ITest
        public static void Add(ITest connector)
        {
            if (!Exists(connector.GetName())) { Connectors.Add(connector); }
        }
    }
}
