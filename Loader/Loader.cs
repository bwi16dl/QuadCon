using GenericInfo;
using Kodi;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;
using Weather;

namespace MEFLoader
{
    // Loads dll files from a folder indicated in PATH variable below
    public class Loader
    {
        // NOTE: there are 2 paths defined, one is used for development and testing, and the other one is used for target solution installed with installers
        //private string path = @"..\..\..\LOADER\CONNECTORS";
        private string path = @"LOADER\CONNECTORS"; //string that has to be used when we deploy the software via installer!

        // MEF imports connectors to 4 identical lists, supporting source interfaces
        [ImportMany(typeof(ITest))]
        public List<ITest> Test { get; set; }
        [ImportMany(typeof(IWeather))]
        public List<IWeather> Weather { get; set; }
        [ImportMany(typeof(IKodi))]
        public List<IKodi> Kodi { get; set; }
        [ImportMany(typeof(IGenericInfo))]
        public List<IGenericInfo> GenericInfo { get; set; }

        // Actual import of dlls
        public void LoadCatalog()
        {
            var catalog = new DirectoryCatalog(path);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        // Just some initialization of empty lists
        public Loader()
        {
            Test = new List<ITest>();
            Kodi = new List<IKodi>();
            Weather = new List<IWeather>();
            Test = new List<ITest>();
        }
    }
}
