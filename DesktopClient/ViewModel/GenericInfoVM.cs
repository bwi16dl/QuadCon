using DesktopClient.DataTypes;
using DesktopClient.GenericInfoService;
using DesktopClient.KodiService;
using DesktopClient.Messaging;
using DesktopClient.WeatherService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DesktopClient.ViewModel
{

    public class GenericInfoVM : ViewModelBase
    {
        #region SERVICE REFERENCES
        private GenericInfoServiceClient client = new GenericInfoServiceClient();
        #endregion

        #region MESSENGER
        Messenger messenger;
        private void SendMessage(MessageItem message) { messenger.Send(new PropertyChangedMessage<MessageItem>(null, message, "")); }
        #endregion

        #region PROPERTIES
        private string ronQuote;

        private bool _kodiService;
        private bool _weatherService;
        private bool _genericService;

        public bool KodiService {
            get { return _kodiService; }
            set { _kodiService = value; RaisePropertyChanged(); } 
        }
        public bool WeatherService {
            get { return _weatherService; }
            set { _weatherService = value; RaisePropertyChanged(); }
        }
        public bool GenericService {
            get { return _genericService; }
            set { _genericService = value; RaisePropertyChanged(); }
        }

        public string RonQuote
        {
            get { return ronQuote; }
            set { ronQuote = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<Error> ErrorList { get; set; }

        public RelayCommand SimulatePort { get; set; }
        public RelayCommand SimulateService { get; set; }
        public RelayCommand<Error> DeleteMessage { get; set; }
        #endregion

        #region TIMERS
        private DispatcherTimer getRonQuote;
        private DispatcherTimer checkServices;
        private DispatcherTimer isup;
        #endregion

        public GenericInfoVM()
        {

        #region INSTANTIATION
        ErrorList = new ObservableCollection<Error>();

        messenger = SimpleIoc.Default.GetInstance<Messenger>();

        KodiService = true;
        WeatherService = true;
        GenericService = true;

        checkServices = new DispatcherTimer();
        checkServices.Interval = new TimeSpan(0, 0, 5);
        checkServices.Tick += new EventHandler(PollServices);

        getRonQuote = new DispatcherTimer();
        getRonQuote.Start();
        getRonQuote.Interval = new TimeSpan(0, 1, 0);
        getRonQuote.Tick += new EventHandler(getQuote);

        isup = new DispatcherTimer();
        isup.Start();
        isup.Interval = new TimeSpan(0, 0, 5);
        isup.Tick += new EventHandler(isServiceUp);
            #endregion

            DeleteMessage = new RelayCommand<Error>((p) => 
            {
                switch (p.SourceName)
                {
                    case "Weather Service":
                        WeatherService = true;
                        break;
                    case "Kodi Service":
                        KodiService = true;
                        break;
                    default:
                        GenericService = true;
                        break;
                }

                foreach (var item in ErrorList)
                {
                    if (item.Date.Equals(p.Date))
                    {
                        ErrorList.Remove(item);
                        break;
                    }
                }
            });


        SimulateService = new RelayCommand(() =>
        {
            client.SetInfo("Generic Info", new GenericInfo() { SourceName = "Weather Service", Date = DateTime.Now.ToString(), SourceError = "Weather Service is down"});
            WeatherService = false;
            SendMessage(new MessageItem("Generic Info", "Generic Info was sent to the server"));
            checkServices.Start();
        },
        () => { return true; });
    }

        //checks if the service is up by pinging the server
        private void isServiceUp(object sender, EventArgs e)
        {
            try
            {
                bool GenericDown = client.GetIsUp("Generic Info");
            }
            catch (Exception)
            {
                ErrorList.Add(new Error("Weather Service", DateTime.Now.ToString(), "Weather Service is down", new BitmapImage(new Uri(FindImageUrl("Weather Service"), UriKind.Relative))));
                ErrorList.Add(new Error("Kodi Service", DateTime.Now.ToString(), "Kodi Service is down", new BitmapImage(new Uri(FindImageUrl("Kodi Service"), UriKind.Relative))));
                ErrorList.Add(new Error("Generic Info Service", DateTime.Now.ToString(), "Generic Info Service is down", new BitmapImage(new Uri(FindImageUrl("Generic Service"), UriKind.Relative))));

                SendMessage(new MessageItem("Generic Info", "Generic Info service is down, please call support"));
                SendMessage(new MessageItem("Weather Service", "Weather service is down, please call support"));
                SendMessage(new MessageItem("Kodi Service", "Kodi service is down, please call support"));

                GenericService = false;
                WeatherService = false;
                KodiService = false;
            }
        }

        /*
        sends error info if there is an error in the service
        generic service is still up here therefore it can
        fetch the error and display it
        */
        private void PollServices(object sender, EventArgs e)
        {
            var temp = client.GetInfo("Generic Info");

            if (temp != null)
            {
                ErrorList.Add(new Error(temp.SourceName, temp.Date, temp.SourceError, new BitmapImage(new Uri(FindImageUrl(temp.SourceName), UriKind.Relative))));
                SendMessage(new MessageItem("Generic Info", "Generic Info was received from the server"));
                checkServices.Stop();
            }
        }

        private void getQuote(object sender, EventArgs e)
        {
            string quote = client.GetRonsQuote("Generic Info");
            RonQuote = quote;
        }

        private string FindImageUrl(string sourceName)
        {
            switch (sourceName)
            {
                case "Weather Service":
                    return "../Images/weather.png";
                case "Kodi Service":
                    return "../Images/kodi.png";
                default:
                    return "../Images/exclamation.jpg";
            }
        }
    }
}