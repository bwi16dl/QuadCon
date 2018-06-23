using System;
using System.Collections.Generic;
using DesktopClient.Messaging;
using DesktopClient.WeatherService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace DesktopClient.ViewModel
{
    //Weathersource implemented by Julian Kocher / wi16b139
    public class WeatherVM : ViewModelBase
    {
        #region PROPERTIES 
        private string weatherreturn; //This string returns any result of queried data and can be linked to GUI output.
        public string Weatherreturn
        {
            get { return weatherreturn; }
            set { weatherreturn = value; RaisePropertyChanged(); }
        }
        private string city;
        public string City
        {
            get { return city; }
            set { city = value; RaisePropertyChanged(); }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set { country = value; RaisePropertyChanged(); }
        }
        private WeatherServiceClient client = new WeatherServiceClient();

        public string Name
        {
            get { return client.GetName("Weather Source from Yahoo"); } //Gets the name of the weather source.
            set { } //omitted
        }

        //private List<string> listbox; //Obsolete?

       

        #endregion

        #region MESSENGER
        Messenger messenger;
        private void SendMessage(MessageItem message) { messenger.Send(new PropertyChangedMessage<MessageItem>(null, message, "")); }
        #endregion


        #region RELAYCOMMANDS
        public RelayCommand FullWeather { get => fullWeather; set => fullWeather = value; }
        public RelayCommand Forecast { get => forecast; set => forecast = value; }
        public RelayCommand Current { get => current; set => current = value; }
        public RelayCommand Wind { get => wind; set => wind = value; }

        RelayCommand fullWeather;
        RelayCommand forecast;
        RelayCommand current;
        RelayCommand wind;
        #endregion

        #region WEATHERVM
        public WeatherVM()
        {
            //Instance the messenger:
            messenger = SimpleIoc.Default.GetInstance<Messenger>();
            //Commands:
            FullWeather = new RelayCommand(method01_fullweather);
            Forecast = new RelayCommand(method02_forecastedweather);
            Current = new RelayCommand(method03_currentweather);
            Wind = new RelayCommand(method04_wind);

        }
        #endregion

        #region Command Implementation
        private void method01_fullweather()
        {
            Weatherreturn = "";
            Weatherreturn = client.QueryWeather("Weather Source from Yahoo", City, Country);
            SendMessage(new MessageItem("info", "Full weather conditions for " + City + "" + Country + " were queried successfully"));
        }

        private void method02_forecastedweather()
        {
            Weatherreturn = "";
            Weatherreturn = client.ForecastedWeather("Weather Source from Yahoo", City, Country);
            SendMessage(new MessageItem("info", "Forecasted weather conditions for " + City + "" + Country + " were queried successfully"));
        }
        private void method03_currentweather()
        {
            Weatherreturn = "";
            Weatherreturn = client.CurrentWeather("Weather Source from Yahoo", City, Country);
            SendMessage(new MessageItem("info", "Current weather conditions for "+City+""+Country+" were queried successfully"));
        }
        private void method04_wind()
        {
            Weatherreturn = "";
            Weatherreturn = client.GetWind("Weather Source from Yahoo", City, Country);
            SendMessage(new MessageItem("info", "Wind conditions for " + City + "" + Country + " were queried successfully"));
        }
        #endregion

    }
}