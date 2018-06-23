using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    [InheritedExport(typeof(IWeather))]
    public interface IWeather
    {
        string GetName();

        // ADD INTERFACE DEFINITION
        //Julian adds interface stuff similar to testsource01


        string QueryWeather(string city, string country);
        string ForecastedWeather(string city, string country);
        string CurrentWeather(string city, string country);
        string GetWind(string city, string country);

    }
}
