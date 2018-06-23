using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Test;


namespace Controller.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Kodi" in both code and config file together.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class KodiService : IKodiService
    {
        public string GetName(string sourceName) { return KodiObject.Find(sourceName).GetName(); }

        public IDictionary<int, string> GetSongs(string sourceName) { return KodiObject.Find(sourceName).GetSongs(); }

        public void SetName(string sourceName, string name) { KodiObject.Find(sourceName).SetName(name); }

        public void PlaySong(string sourceName, int songId) { KodiObject.Find(sourceName).PlaySong(songId); }

        public IDictionary<string, string> GetPictures(string sourceName) { return KodiObject.Find(sourceName).GetPictures(); }

        public IDictionary<int, string> GetMovies(string sourceName) { return KodiObject.Find(sourceName).GetMovies(); }

        public void PlayMovie(string sourceName, int movieId) { KodiObject.Find(sourceName).PlayMovie(movieId); }


        public IDictionary<string, string> GetPlaylists(string sourceName) { return KodiObject.Find(sourceName).GetPlaylists(); }


        public void PlayPause(string sourceName) { KodiObject.Find(sourceName).PlayPause(); }

        public void PlayPlaylist(string sourceName, string playlistId) { KodiObject.Find(sourceName).PlayPlaylist(playlistId); }

        public void PlayPicture(string sourceName, string pictureId) { KodiObject.Find(sourceName).PlayPicture(pictureId); }
        public void ChangeVolume(string sourceName, int volume) { KodiObject.Find(sourceName).ChangeVolume(volume); }
    }
}
