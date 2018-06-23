using DesktopClient.Messaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using DesktopClient.KodiService;
namespace DesktopClient.ViewModel
{
    
    public class KodiVM : ViewModelBase
    {
        private KodiServiceClient client = new KodiServiceClient();

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /*
        public string Name
        {
            get { return client.GetName("Kodi Source 01"); }
            set { client.SetName("Kodi Source 01", value); RaisePropertyChanged(); }
        }
        */

        private int volume;

        public int Volume
        {
            get { return volume; }
            set {
                volume = value; RaisePropertyChanged("Volume");
                client.ChangeVolume(Name, volume);
            }
        }

        private IDictionary<int, string> songs;

        public IDictionary<int, string> Songs
        {
            get { return songs; }
            set { songs = value; RaisePropertyChanged(); }
        }

        private IDictionary<int, string> videos;

        public IDictionary<int, string> Videos
        {
            get { return videos; }
            set { videos = value; RaisePropertyChanged(); }
        }

        private IDictionary<string, string> photos;

        public IDictionary<string, string> Photos
        {
            get { return photos; }
            set { photos = value; RaisePropertyChanged(); }
        }

        private IDictionary<string, string> playlists;

        public IDictionary<string, string> Playlists
        {
            get { return playlists; }
            set { playlists = value; RaisePropertyChanged(); }
        }


        private RelayCommand updateSongsList;
        public RelayCommand UpdateSongsList
        {
            get { return updateSongsList; }
            set { updateSongsList = value; }
        }

        private RelayCommand refresh;
        public RelayCommand Refresh
        {
            get { return refresh; }
            set { refresh = value; }
        }


        private RelayCommand playSong;
        public RelayCommand PlaySong
        {
            get { return playSong; }
            set { playSong = value; }
        }

        private RelayCommand playVideo;
        public RelayCommand PlayVideo
        {
            get { return playVideo; }
            set { playVideo = value; }
        }

        private RelayCommand playPhoto;
        public RelayCommand PlayPhoto
        {
            get { return playPhoto; }
            set { playPhoto = value; }
        }

        private RelayCommand playPlaylist;
        public RelayCommand PlayPlaylist
        {
            get { return playPlaylist; }
            set { playPlaylist = value; }
        }

        private RelayCommand playPause;
        public RelayCommand PlayPause
        {
            get { return playPause; }
            set { playPause = value; }
        }

      

        private object selectedSong;

        public object SelectedSong
        {
            get { return selectedSong; }
            set { selectedSong = value; RaisePropertyChanged("SelectedSong");  }
        }

        private object selectedVideo;

        public object SelectedVideo
        {
            get { return selectedVideo; }
            set { selectedVideo = value; RaisePropertyChanged("SelectedMovie"); }
        }

        private object selectedPhoto;

        public object SelectedPhoto
        {
            get { return selectedPhoto; }
            set { selectedPhoto = value; RaisePropertyChanged("SelectedPhoto"); }
        }

        private object selectedPlaylist;

        public object SelectedPlaylist
        {
            get { return selectedPlaylist; }
            set { selectedPlaylist = value; RaisePropertyChanged("SelectedPlaylist"); }
        }

        Messenger messenger;
        private void SendMessage(MessageItem message) { messenger.Send(new PropertyChangedMessage<MessageItem>(null, message, "")); }

        public KodiVM()
        {
            Name = "Kodi Source 01";
            Songs = client.GetSongs(Name);
            Videos = client.GetMovies(Name);
            Photos = client.GetPictures(Name);
            Playlists = client.GetPlaylists(Name);
            Volume = 90;
            messenger = SimpleIoc.Default.GetInstance<Messenger>();
            SelectedSong = null;
            SelectedPhoto = null;
            SelectedVideo = null;
            SelectedPlaylist = null;

            PlaySong = new RelayCommand(
                () => {
                    
                     client.PlaySong(Name, ((KeyValuePair<int, string>)SelectedSong).Key );
                    // TODO
                    SendMessage(new MessageItem("info", "Kodi: Playing Song " + ((KeyValuePair<int, string>)SelectedSong).Value));
                },
                () => { return SelectedSong != null; });

            PlayVideo = new RelayCommand(
               () => {

                   client.PlayMovie(Name, ((KeyValuePair<int, string>)SelectedVideo).Key);
                    // TODO
                    SendMessage(new MessageItem("info", "Kodi: Playing Video " + ((KeyValuePair<int, string>)SelectedVideo).Value));
               },
               () => { return SelectedVideo != null; });

            PlayPhoto = new RelayCommand(
               () => {

                    client.PlayPicture(Name, ((KeyValuePair<string, string>)SelectedPhoto).Key);
                    // TODO
                    SendMessage(new MessageItem("info", "Kodi: Playing Photo " + ((KeyValuePair<string, string>)SelectedPhoto).Value));
               },
               //() => { return SelectedPhoto != null; });
               () => { return SelectedPhoto != null; });

            PlayPlaylist = new RelayCommand(
               () => {

                   client.PlayPlaylist(Name, ((KeyValuePair<string, string>)SelectedPlaylist).Key);
                    // TODO
                    SendMessage(new MessageItem("info", "Kodi: Playing Playlist " + ((KeyValuePair<string, string>)SelectedPlaylist).Value));
               },
               () => { return SelectedPlaylist != null; });

            PlayPause = new RelayCommand(
               () => {
                   client.PlayPause(Name);
                   // TODO
                   SendMessage(new MessageItem("info", "Kodi: Issued PlayPause Command"));
               },
               () => { return true; });

            Refresh = new RelayCommand(
               () => {
                   Songs = new Dictionary<int, string>();
                   Videos = new Dictionary<int, string>();
                   Photos = new Dictionary<string, string>();
                   Playlists = new Dictionary<string, string>();

                   Songs = client.GetSongs(Name);
                   Videos = client.GetMovies(Name);
                   Photos = client.GetPictures(Name);
                   Playlists = client.GetPlaylists(Name);
                   SelectedSong = null;
                   SelectedPhoto = null;
                   SelectedVideo = null;
                   SelectedPlaylist = null;
                   // TODO
                   SendMessage(new MessageItem("info", "Kodi: Kodi GUI Refreshed"));
               },
               () => { return true; });

        }
    }
}