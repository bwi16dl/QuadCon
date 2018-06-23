using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Controller.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Weather" in both code and config file together.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class WeatherService : IWeatherService
    {
        public string CurrentWeather(string sourceName, string city, string country)
        {
            return WeatherObject.Find(sourceName).CurrentWeather(city, country); //works - Julian
        }

        public string ForecastedWeather(string sourceName, string city, string country)
        {
            return WeatherObject.Find(sourceName).ForecastedWeather(city, country); //works - Julian
        }

        public string GetName(string sourceName)
        {
            return WeatherObject.Find(sourceName).GetName();
        }

        public string GetWind(string sourceName, string city, string country)
        {
            return WeatherObject.Find(sourceName).GetWind(city, country); //works - Julian
        }

        public string QueryWeather(string sourceName, string city, string country)
        {
            return WeatherObject.Find(sourceName).QueryWeather(city, country); //works - Julian
        }

      //  public void SetName(string sourceName, string name)
      //  {
            //i dont set names
      //      throw new NotImplementedException();
     //   }

      
    }
}
