using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodi
{
    [InheritedExport(typeof(IKodi))]
    public interface IKodi
    {
        string GetName();
        void SetName(string name);
        // ADD INTERFACE DEFINITION

        // User Functions
        IDictionary<int, string> GetSongs();
        void PlaySong(int songId);

        IDictionary<string, string> GetPictures();
        void PlayPicture(string pictureId);

        IDictionary<int, string> GetMovies();
        void PlayMovie(int movieId);

        IDictionary<string, string> GetPlaylists();
        void PlayPlaylist(string playlistId);

        void PlayPause();

        void ChangeVolume(int volume);
    }
}
