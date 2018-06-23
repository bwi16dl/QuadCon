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
    public class Loader
    {
        //private string path = @"..\..\..\LOADER\CONNECTORS";
        private string path = @"LOADER\CONNECTORS"; //string that has to be used when we deploy the software via installer!

        [ImportMany(typeof(ITest))]
        public List<ITest> Test { get; set; }
        [ImportMany(typeof(IWeather))]
        public List<IWeather> Weather { get; set; }
        [ImportMany(typeof(IKodi))]
        public List<IKodi> Kodi { get; set; }
        [ImportMany(typeof(IGenericInfo))]
        public List<IGenericInfo> GenericInfo { get; set; }

        public void LoadCatalog()
        {
            var catalog = new DirectoryCatalog(path);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public Loader()
        {
            Test = new List<ITest>();
            Kodi = new List<IKodi>();
            Weather = new List<IWeather>();
            Test = new List<ITest>();
        }
    }
}
