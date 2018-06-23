using GenericInfo;
using Kodi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;
using Weather;

namespace Objects
{
    public static class KodiObject
    {
        public static List<IKodi> Connectors = new List<IKodi>();

        public static bool Exists(string name)
        {
            foreach (var i in Connectors) { if (i.GetName().Equals(name)) { return true; } }
            return false;   
        }

        public static IKodi Find(string name)
        {
            foreach (var i in Connectors) { if (i.GetName().Equals(name)) { return i; } }
            return null;
        }

        public static void Add(IKodi connector)
        {
            if (!Exists(connector.GetName())) { Connectors.Add(connector); }
        }
    }
}
