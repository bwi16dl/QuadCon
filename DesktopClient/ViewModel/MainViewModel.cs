using DesktopClient.Messaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace DesktopClient.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private DispatcherTimer clockTimer;

        #region MULTIVM
        private ViewModelBase activeVM;
        public ViewModelBase ActiveVM
        {
            get { return activeVM; }
            set { activeVM = value; RaisePropertyChanged(); }
        }

        private void UpdateUserControl(string vm)
        {
            if (vm.Equals("Admin")) { ActiveVM = SimpleIoc.Default.GetInstance<AdminVM>(); }
            if (vm.Equals("Kodi")) { ActiveVM = SimpleIoc.Default.GetInstance<KodiVM>(); }
            if (vm.Equals("Weather")) { ActiveVM = SimpleIoc.Default.GetInstance<WeatherVM>(); }
            if (vm.Equals("GenericInfo")) { ActiveVM = SimpleIoc.Default.GetInstance<GenericInfoVM>(); }
            if (vm.Equals("Test")) { ActiveVM = SimpleIoc.Default.GetInstance<TestVM>(); }

            messenger.Send(new PropertyChangedMessage<MessageItem>(null, new MessageItem("info", vm + " was activated"), ""));
        }
        #endregion

        #region PROPERTIES
        private List<string> userControls;
        public List<string> UserControls
        {
            get { return userControls; }
            set { userControls = value; }
        }

        private string selectedVM;
        public string SelectedVM
        {
            get { return selectedVM; }
            set { selectedVM = value; UpdateUserControl(selectedVM); }
        }

        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; RaisePropertyChanged(); }
        }
        #endregion

        #region MESSENGER
        private Messenger messenger;

        private ObservableCollection<MessageItem> messages = new ObservableCollection<MessageItem>();
        public ObservableCollection<MessageItem> Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        public void ProcessMessage(PropertyChangedMessage<MessageItem> message)
        {
            MessageItem item = message.NewValue;
            Messages.Add(item);
            new Thread(() =>
            {
                Thread.Sleep(5000);
                Application.Current.Dispatcher.Invoke(() => { Messages.Remove(item); }); }).Start();
        }
        #endregion

        public MainViewModel()
        {

            clockTimer = new DispatcherTimer();
            clockTimer.Start();
            clockTimer.Interval = new TimeSpan(0, 0, 5);
            clockTimer.Tick += new EventHandler(updateClock);

            UserControls = new List<string>() { "Admin", "Kodi", "Weather", "GenericInfo", "Test" };

            messenger = SimpleIoc.Default.GetInstance<Messenger>();
            messenger.Register<PropertyChangedMessage<MessageItem>>(this, ProcessMessage);
        }

        private void updateClock(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Time = DateTime.Now.ToString("HH:mm dddd dd MMMM yyyy");
            });
        }
    }


}