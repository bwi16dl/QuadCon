using DesktopClient.Messaging;
using DesktopClient.TestService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;

namespace DesktopClient.ViewModel
{
    
    public class TestVM : ViewModelBase
    {

        private TestServiceClient client = new TestServiceClient();

        #region PROPERTIES
        public string Name
        {
            get { return client.GetName("Test Weather Source 01"); }
            set { client.SetName("Test Weather Source 01", value); RaisePropertyChanged(); }
        }

        private TestData oneDay;
        public TestData OneDay
        {
            get { return oneDay; }
            set { oneDay = value; RaisePropertyChanged(); }
        }

        private List<TestData> threeDays;
        public List<TestData> ThreeDay
        {
            get { return threeDays; }
            set { threeDays = value; RaisePropertyChanged(); }
        }
        #endregion

        #region COMMANDS
        private RelayCommand click;
        public RelayCommand Trigger
        {
            get { return click; }
            set { click = value; }
        }

        private RelayCommand setServerObject;
        public RelayCommand SetServerObject
        {
            get { return setServerObject; }
            set { setServerObject = value; }
        }

        private RelayCommand getServerObject;
        public RelayCommand GetServerObject
        {
            get { return getServerObject; }
            set { getServerObject = value; }
        }
        #endregion

        #region MESSENGER
        Messenger messenger;
        private void SendMessage(MessageItem message) { messenger.Send(new PropertyChangedMessage<MessageItem>(null, message, "")); }
        #endregion

        public TestVM()
        {
            messenger = SimpleIoc.Default.GetInstance<Messenger>();
            
            Trigger = new RelayCommand(
                () => {
                    client.Trigger("Test Weather Source 01", "GUI button was pressed");
                    SendMessage(new MessageItem("info", "Command was triggered on a server"));
                },
                () => { return true; });

            SetServerObject = new RelayCommand(
                () => {
                    client.SetOneDay("Test Weather Source 01", oneDay);
                    client.SetThreeDays("Test Weather Source 01", threeDays);
                    SendMessage(new MessageItem("info", "Server objects were updated with information from GUI"));
                },
                () => { return true; });

            GetServerObject = new RelayCommand(
                () => {
                    OneDay = client.GetOneDay("Test Weather Source 01");
                    ThreeDay = client.GetThreeDays("Test Weather Source 01");
                    SendMessage(new MessageItem("info", "Objects were updated from server"));
                },
                () => { return true; });
        }
    }
}