using ServerClient.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace ServerClient.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<Messenger>(true);
            SimpleIoc.Default.Register<MainViewModel>(true);
            SimpleIoc.Default.Register<KodiVM>(true);
            SimpleIoc.Default.Register<GenericInfoVM>(true);
            SimpleIoc.Default.Register<WeatherVM>(true);
            SimpleIoc.Default.Register<TestVM>(true);
            SimpleIoc.Default.Register<AdminVM>(true);
        }

        public MainViewModel Main { get { return SimpleIoc.Default.GetInstance<MainViewModel>(); } }
        public KodiVM Kodi { get { return SimpleIoc.Default.GetInstance<KodiVM>(); } }
        public WeatherVM Weather { get { return SimpleIoc.Default.GetInstance<WeatherVM>(); } }
        public GenericInfoVM GenericInfo { get { return SimpleIoc.Default.GetInstance<GenericInfoVM>(); } }
        public TestVM Test { get { return SimpleIoc.Default.GetInstance<TestVM>(); } }
        public AdminVM Admin { get { return SimpleIoc.Default.GetInstance<AdminVM>(); } }
        
        public static void Cleanup() { }
    }
}