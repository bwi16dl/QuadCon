using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weather.WeatherSource01
{
    class Weather01 : IWeather
    {
        //The most update query is always on the bottom of the textbox field! 
        private string name = "Weather Source from Yahoo";
        //Strings for internal use
        private string cityy = "";
        private string countryy = "";
        StringBuilder str = new StringBuilder();

        public Weather01()
        {
            SetName("Weather Source from Yahoo");

        }
        public void SetName(string name)
        {
            this.name = name;
            // Console.WriteLine("\n\t=> Name set: " + name);
        }

        //added newtonsoft json nuget stuff
        public string CurrentWeather(string city, string country)
        {
            cityy = city;
            countryy = country;
            string yahooWeatherQuery_current = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + city + "%2C%20" + country + "%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            return CurrentWeather(yahooWeatherQuery_current);
            //Julian: done
        }

        public string ForecastedWeather(string city, string country)
        {
            cityy = city;
            countryy = country;
            string yahooWeatherQuery_forecast = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + city + "%2C%20" + country + "%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            return ForecastedWeather(yahooWeatherQuery_forecast);
            //Julian: done
        }

        public string GetName()
        {
            return name;
        }

        public string GetWind(string city, string country)
        {
            cityy = city;
            countryy = country;
            string yahooWeatherQuery_wind = "https://query.yahooapis.com/v1/public/yql?q=select%20wind%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + city + "%2C%20" + country + "%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            return GetWind(yahooWeatherQuery_wind);
            //Julian: done
        }

        public string QueryWeather(string city, string country)
        {
            string yahooWeatherQuery_current = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + city + "%2C%20" + country + "%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            string yahooWeatherQuery_forecast = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + city + "%2C%20" + country + "%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            string yahooWeatherQuery_wind = "https://query.yahooapis.com/v1/public/yql?q=select%20wind%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + city + "%2C%20" + country + "%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            StringBuilder fullWeather = new StringBuilder();
            fullWeather.Append(CurrentWeather(yahooWeatherQuery_current));
            fullWeather.Append(ForecastedWeather(yahooWeatherQuery_forecast));
            fullWeather.Append(GetWind(yahooWeatherQuery_wind));
            return fullWeather.ToString();
        }





        //sample methods



        /// <summary>
        /// This Method returns a JObject of gathered weatherinformation in a json string that can be traversed by asigning it to a dynamic object. 
        /// </summary>
        /// <param name="query"></param>
        /// <returns>JObject</returns>
        private JObject queryWeatherApi(string query)
        {

            string results = "";
            //create a webRequest 
            using (WebClient wc = new WebClient())
            {
                results = wc.DownloadString(query);
                dynamic json = JObject.Parse(results);
                JObject dataObject = JObject.Parse(results);
                return json;
            }
        }


        /// <summary>
        /// Requests forecasted weather information and handles return data 
        /// </summary>
        /// <param name="yahooWeatherQuery_forecast"></param>
        public string ForecastedWeather(string yahooWeatherQuery_forecast)
        {
            try
            {

                DateTime created = DateTime.Now;
                dynamic json = queryWeatherApi(yahooWeatherQuery_forecast);
                var items = json.query.results.channel.item.forecast;
                int i = 1;
                foreach (var forecast in items)
                {
                    str.AppendLine();
                    str.Append("Forecasted weather for " + cityy+"|"+countryy +" queried at " + DateTime.Now + " | ");
                    str.Append("Forecast for " + forecast.day + ", " + forecast.date + " | ");
                    str.Append("Expected high: " + forecast.high + " | ");
                    str.Append("Expected low: " + forecast.low + " | ");
                    str.Append("Avg. condition: " + forecast.text);

                    i++;
                }
                str.Append("\n");
                return str.ToString();
            }
            catch (Exception error)
            {
                //write to log file
                #region log-file
                string path = @"c:\WeatherService_Log.txt";
                //Initial creation text
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("Logfile for errors of Weatherservice:"); //headline 
                    }
                }

                // Append information of weather requests over time
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("An error for FORECASTED_WEATHER_INFORMATION occurred at " + DateTime.Now);
                    sw.WriteLine("Error information: " + error.Message + " Please contact your system provider to resolve the issue.");
                    sw.WriteLine();
                }
                #endregion

                return "Error occured while processing request. For details see log information @input path to \\WeatherService_Log.txt"; //TODO: Input Path to Log File !!
            }
        }



        /// <summary>
        /// Requests current Weather Information and handles the return data for Console and File output
        /// //File writing currently disabled!
        /// </summary>
        /// <param name="yahooWeatherQuery_current"></param>
        public string CurrentWeather(string yahooWeatherQuery_current)
        {
            DateTime created = DateTime.Now;

            try
            {
                dynamic json = queryWeatherApi(yahooWeatherQuery_current);


                //Write JSON object to a File
                //File.WriteAllText(@"c:\weatherdata.json", <dataObject>.ToString());

                var items = json.query.results.channel.item.condition;
                var code = items.code; //Not used for now.
                var temp = items.temp;
                var text = items.text;

                // TODO:
                //compare
                //if value changed
                //update GUI
                //Requests are still written to history anyway!

                //Append data
                #region Output
                str.AppendLine();
                str.Append("Current weather for " + cityy + "|" + countryy + " queried at " + created + " | ");
                str.Append("Current temperature: " + temp + " | ");
                str.Append("Current weather condition: " + text);
                str.Append("\n");
                return str.ToString();
                #endregion

                /*
                //Obsolete??
                //Log information
                // TODO: Adapt for DB
                #region Fileoutput
                string path = @"c:\MyTest.txt"; //TODO: Input Server FilePath
                //Initial creation text
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("Logging weather information:");
                    }
                }

                // Append information of weather requests over time
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("New weather request");
                    sw.WriteLine("Request received at: " + created);
                    sw.WriteLine("Current temperature: " + temp);
                    sw.WriteLine("Current weather condition: " + text);
                }
                #endregion
                */
            }
            catch (Exception error)
            {
                //write to log file
                #region log-file 
                string path = @"c:\WeatherService_Log.txt"; //TODO: Input SErver Path to log file!
                //Initial creation text
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("Logfile for errors of Weatherservice:"); //headline 
                    }
                }

                // Append information of weather requests over time
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("An error for CURRENT_WEATHER_INFORMATION occurred at " + DateTime.Now);
                    sw.WriteLine("Error information: " + error.Message + " Please contact your system provider to resolve the issue.");
                    sw.WriteLine();
                }
                #endregion

                return "Error occured while processing request. For details see log information @C:\\WeatherService_Log.txt"; //TODO: Change Path!
            }


        }


        /// <summary>
        /// Requests current wind information and handles return data 
        /// </summary>
        /// <param name="yahooWeatherQuery_wind"></param>
        public string GetWind(string yahooWeatherQuery_wind)
        {

            DateTime created = DateTime.Now;

            try
            {
                dynamic json = queryWeatherApi(yahooWeatherQuery_wind);
                var items = json.query.results.channel.wind;

                str.AppendLine();
                str.Append("Windconditions for " + cityy + "|" + countryy + " queried at " + created + " | ");
                str.Append("Current chill " + items.chill + " | ");
                str.Append("Current wind direction " + items.direction + " | ");
                str.Append("Current wind speed " + items.speed);
                str.Append("\n");
                return str.ToString();

            }
            catch (Exception error)
            {
                //write to log file
                #region log-file 
                string path = @"c:\WeatherService_Log.txt";
                //Initial creation text
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("Logfile for errors of Weatherservice:"); //headline 
                    }
                }

                // Append information of weather requests over time
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("An error for WIND_INFORMATION occurred at " + DateTime.Now);
                    sw.WriteLine("Error information: " + error.Message + " Please contact your system provider to resolve the issue.");
                    sw.WriteLine();
                }
                #endregion
                return "Error occured while processing request. For details see log information @C:\\WeatherService_Log.txt";
            }



        }


    }
}
