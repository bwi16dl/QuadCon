using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Weather;

namespace Controller.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWeather" in both code and config file together.
    [ServiceContract]
    public interface IWeatherService
    {
        [OperationContract]
        string GetName(string sourceName);

       // [OperationContract]
       // void SetName(string sourceName, string name);

        [OperationContract]
        string CurrentWeather(string sourceName, string city, string country);

        [OperationContract]
        string ForecastedWeather(string sourceName, string city, string country);

        [OperationContract]
        string GetWind(string sourceName, string city, string country);

        [OperationContract]
        string QueryWeather(string sourceName, string city, string country);

    }
}
