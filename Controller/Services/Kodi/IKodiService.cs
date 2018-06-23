using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Controller.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IKodi" in both code and config file together.
    [ServiceContract]
    public interface IKodiService
    {
        [OperationContract]
        string GetName(string sourceName);

        [OperationContract]
        void SetName(string sourceName, string name);

        [OperationContract]
        IDictionary<int, string> GetSongs(string sourceName);


        [OperationContract]
        void PlaySong(string sourceName, int songId);

        [OperationContract]
        IDictionary<string, string> GetPictures(string sourceName);

        [OperationContract]
        IDictionary<int, string> GetMovies(string sourceName);

        [OperationContract]
        void PlayMovie(string sourceName, int movieId);

        [OperationContract]
        IDictionary<string, string> GetPlaylists(string sourceName);

        [OperationContract]
        void PlayPause(string sourceName);

        [OperationContract]
        void PlayPlaylist(string sourceName, string playlistId);

        [OperationContract]
        void PlayPicture(string sourceName, string pictureId);

        [OperationContract]
        void ChangeVolume(string sourceName, int volume);
    }
}
