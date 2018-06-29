using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using GenericInfo.Data;

namespace GenericInfo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GenericInfoService" in both code and config file together.
    public class GenericInfoSource : IGenericInfo
    {
        private string name;
        private Data.GenericInfo info;
        private HttpClient client;
        //the url string to access the quote api
        private const string url = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";

        #region GETTERS

        //get the name of the service
        public string GetName() { return name; }
        public Data.GenericInfo GetInfo()
        {
            return info;
        }
        #endregion

        #region SETTERS

        //shows the error in the controller
        public void SetName(string name)
        {
            this.name = name;
            // Console.WriteLine("\n\t=> Info Name set: " + name);
        }
        public void SetInfo(Data.GenericInfo info)
        {
            this.info = info;
            Console.WriteLine("\n\t=> Error: \n\t=> " 
                + " " + info.SourceName
                + " " + info.Date
                + " " + info.SourceError
                );
        }
        #endregion

        /*
        test code to testthe generic info source is sending data back to GUI
        private void StartUpdating()
        {
            Random random = new Random();

            Data.GenericInfo[] infoList = new Data.GenericInfo[]
            {
                new Data.GenericInfo(){SourceName="Weather", Date=DateTime.Now.ToString(), SourceError="Unable to start service"},
                new Data.GenericInfo(){SourceName="Kodi", Date=DateTime.Now.ToString(), SourceError="Unable to start service"},
                new Data.GenericInfo(){SourceName="Generic Info", Date=DateTime.Now.ToString(), SourceError="Service not available"},
                new Data.GenericInfo(){SourceName="Weather", Date=DateTime.Now.ToString(), SourceError="Unable to start service"},
                new Data.GenericInfo(){SourceName="Weather", Date=DateTime.Now.ToString(), SourceError="Unable to start service"},
                new Data.GenericInfo(){SourceName="Weather", Date=DateTime.Now.ToString(), SourceError="Unable to start service"}
            };

            while (true)
            {
                SetInfo(infoList[random.Next(0,5)]);
                Thread.Sleep(20000);
            };

        }
        */

        //retrieve a quote from the the API
        public string GetRonsQuote()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                string removeBlockQuotes = data.Trim('[', ']');
                return removeBlockQuotes;
            }
            else
            {
                return "Ron is currently chopping wood and drinking Whisky while wrestling a bear";
            }

        }

        //checking if a service is up
        public bool Isup()
        {
            return true;
        }

        public GenericInfoSource()
        {
            SetName("Generic Info");
        }


    }
}
