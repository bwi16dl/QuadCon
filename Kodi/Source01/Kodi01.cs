using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kodi
{
    class Kodi01 : IKodi
    {

        public Kodi01()
        {
            SetName("Kodi Source 01");
            this.url = @"http://localhost:8080";
        }

        #region FIELDS
        private string url = @"http://localhost:8080";
        private string name;
        private IDictionary<int, string> songs;
        #endregion

        #region GETTERS
        public string GetName()
        {
            return this.name;
        }

        public IDictionary<int, string> GetSongs()
        {
            if (!Ping()) { return null; }

            string html = HttpGet(url + "/jsonrpc?request={%22jsonrpc%22:%222.0%22,%22id%22:1,%22method%22:%22AudioLibrary.GetSongs%22,%22params%22:{}}");
            dynamic json = System.Web.Helpers.Json.Decode(@html);
            try
            {
                IDictionary<int, string> results = new Dictionary<int, string>();
                for (int i = 0; i < json.result.songs.Length; i++)
                {
                    results.Add(json.result.songs[i].songid, json.result.songs[i].label);
                }
                return results;
            }
            catch (Exception e) { return null; }
        }
        public IDictionary<string, string> GetPictures()
        {
            if (!Ping()) { return null; }

            string html = HttpGet(url + "/jsonrpc?request={%22jsonrpc%22:%222.0%22,%22id%22:1,%22method%22:%22Files.GetSources%22,%22params%22:{%22media%22:%22pictures%22}}");
            //http://localhost:8080/jsonrpc?request={%22jsonrpc%22:%222.0%22,%22id%22:1,%22method%22:%22Files.GetSources%22,%22params%22:{%22media%22:%22pictures%22}}

            void subdirCheck(string dir, ref IDictionary<string, string> results2)
            {
                string html2 = HttpGet(url + "/jsonrpc?request={\"jsonrpc\":\"2.0\",\"id\":1,\"method\":\"Files.GetDirectory\",\"params\":{\"directory\":\"" + dir.Replace("\\", "/") + "\",\"media\":\"pictures\"}}").Replace("\\\\\\", "\\");
                // /jsonrpc?request={"jsonrpc":"2.0","id":1,"method":"Files.GetDirectory","params":{"directory":"D://Pictures//Camera","media":"pictures"}}
                dynamic json2 = System.Web.Helpers.Json.Decode(@html2);
                if (json2.result.files == null) { return; }
                for (int i = 0; i < json2.result.files.Length; i++)
                {
                    if (((string)(json2.result.files[i].file)).EndsWith("\\"))
                    {
                        string tmp = json2.result.files[i].file;
                        subdirCheck(json2.result.files[i].file, ref results2);
                    }
                    else
                    {
                        if (!"Picture add-ons".Equals(json2.result.files[i].label))
                        {
                            try
                            {
                                results2.Add(json2.result.files[i].file, json2.result.files[i].label);
                            }
                            catch (Exception e)
                            {

                            }
                        }


                    }
                }
            }
            dynamic json = System.Web.Helpers.Json.Decode(@html);
            IDictionary<string, string> results = new Dictionary<string, string>();
            try
            {
                for (int i = 0; i < json.result.sources.Length; i++)
                {
                    string tmp = (string)(json.result.sources[i].file);
                    if (tmp.EndsWith("\\"))
                    {
                        subdirCheck(json.result.sources[i].file, ref results);
                    }
                    else if (!"Picture add-ons".Equals(json.result.sources[i].label))
                    {

                        results.Add(json.result.sources[i].file, json.result.sources[i].label);
                    }

                }
                return results;
            }
            catch (Exception e) { return null; }

        }
        public IDictionary<int, string> GetMovies()
        {
            if (!Ping()) { return null; }

            string html = HttpGet(url + "/jsonrpc?request={%22jsonrpc%22:%222.0%22,%22id%22:1,%22method%22:%22VideoLibrary.GetMovies%22,%22params%22:{}}");
            dynamic json = System.Web.Helpers.Json.Decode(@html);
            //Console.Write(json.result);
            //Console.ReadLine();
            try
            {
                IDictionary<int, string> results = new Dictionary<int, string>();
                for (int i = 0; i < json.result.movies.Length; i++)
                {
                    results.Add(json.result.movies[i].movieid, json.result.movies[i].label);
                }
                return results;
            }
            catch (Exception e) { return null; }



        }

        public IDictionary<string, string> GetPlaylists()
        {
            if (!Ping()) { return null; }

            string html = HttpGet(url + "/jsonrpc?request={\"jsonrpc\":\"2.0\",\"method\":\"Files.GetDirectory\",\"id\":\"1527438395460\",\"params\":{\"directory\":\"special://profile/playlists/music\",\"media\":\"music\",\"properties\":[\"title\",\"file\",\"mimetype\",\"thumbnail\",\"dateadded\"],\"sort\":{\"method\":\"none\",\"order\":\"ascending\"}}}");
            dynamic json = System.Web.Helpers.Json.Decode(@html);

            IDictionary<string, string> results = new Dictionary<string, string>();
            try
            {
                for (int i = 0; i < json.result.files.Length; i++)
                {
                    results.Add(json.result.files[i].file, json.result.files[i].label);
                }
                return results;
            }
            catch (Exception e) { return null; }
        }

        #endregion

        #region SETTERS
        public void SetName(string name)
        {
            this.name = name;
            Console.WriteLine("\n\t=> Name set: " + name);
            songs = GetSongs();
            Console.WriteLine("\n\t=> Songs: " + name);

        }
        #endregion

        public void SetUrl(string url)
        {
            this.url = url;
        }
        public void PlaySong(int songId)
        {
            if (!Ping()) { return; }
            HttpGet(url + "/jsonrpc?request={%22jsonrpc%22:%222.0%22,%22id%22:1,%22method%22:%22Playlist.Clear%22,%22params%22:{%22playlistid%22:0}}");
            HttpGet(url + "/jsonrpc?request=%7B%20%22jsonrpc%22%3A%20%222.0%22%2C%20%22id%22%3A%201%2C%20%22method%22%3A%20%22Playlist.Add%22%2C%20%22params%22%3A%20%7B%20%22playlistid%22%3A%200%2C%20%22item%22%3A%20%7B%20%22songid%22%3A%20" + songId + "} } }");
            HttpGet(url + "/jsonrpc?request=%7B%22jsonrpc%22%3A%20%222.0%22%2C%20%22id%22%3A%202%2C%20%22method%22%3A%20%22Player.Open%22%2C%20%22params%22%3A%20%7B%22item%22%3A%20%7B%22playlistid%22%3A%200%7D%7D%7D");
        }

        public void PlayMovie(int movieId)
        {
            if (!Ping()) { return; }
            HttpGet(url + "/jsonrpc?request={%22jsonrpc%22:%222.0%22,%22id%22:%221%22,%22method%22:%22Player.Stop%22,%22params%22:{%22playerid%22:2}}");

            // http://localhost:8080/jsonrpc?request={%22jsonrpc%22:%222.0%22,%22id%22:%221%22,%22method%22:%22Player.Stop%22,%22params%22:{%22playerid%22:2}}
            HttpGet(url + "/jsonrpc?request=%7B%22jsonrpc%22%3A%20%222.0%22%2C%20%22id%22%3A%202%2C%20%22method%22%3A%20%22Player.Open%22%2C%20%22params%22%3A%20%7B%22item%22%3A%20%7B%22movieid%22%3A%20" + movieId + "%7D%7D%7D");
        }


        public void PlayPause()
        {
            if (!Ping()) { return; }
            HttpGet(url + "/jsonrpc?request={\"jsonrpc\": \"2.0\", \"method\": \"Player.PlayPause\", \"params\": { \"playerid\": 0 }, \"id\": 1}");
            HttpGet(url + "/jsonrpc?request={\"jsonrpc\": \"2.0\", \"method\": \"Player.PlayPause\", \"params\": [ 1,\"toggle\"], \"id\": 1}");
            //[{"jsonrpc":"2.0","method":"Player.PlayPause","params":[1,"toggle"],"id":13}]
        }

        public void ChangeVolume(int volume)
        {
            if (!Ping()) { return; }
            HttpGet(url + "/jsonrpc?request={\"jsonrpc\":\"2.0\",\"id\":1,\"method\":\"Application.SetVolume\",\"params\":{\"volume\":" + volume + "}}");
            ///jsonrpc?request={"jsonrpc":"2.0","id":1,"method":"Application.SetVolume","params":{"volume":100}}
        }
        public void PlayPicture(string pictureId)
        {
            if (!Ping()) { return; }

            HttpGet(url + "/jsonrpc?request={\"jsonrpc\":\"2.0\",\"id\":\"1\",\"method\":\"Player.Open\",\"params\":{\"item\":{\"file\":\"" + pictureId.Replace("\\", "/") + "\"}}}");
            //{"jsonrpc":"2.0","id":"1","method":"Player.Open","params":{"item":{"file":"D:\Pictures\Wallapapers\anton-fadeev-image.jpg"}}}
        }

        private bool Ping()
        {
            try
            {
                string pingResult = HttpGet(url + "/jsonrpc?request={\"jsonrpc\":\"2.0\",\"method\":\"JSONRPC.Ping\",\"params\":[],\"id\":1}");
                if (pingResult.Equals("") || pingResult == null) { return false; }
                return true;
            }
            catch (Exception e)
            {
            }
            return false;

        }

        public void PlayPlaylist(string playlistId)
        {
            if (!Ping()) { return; }
            HttpGet(url + "/jsonrpc?request={\"jsonrpc\":\"2.0\",\"method\":\"Playlist.Clear\",\"params\":[0],\"id\":10}");
            HttpGet(url + "/jsonrpc?request={\"jsonrpc\":\"2.0\",\"method\":\"Playlist.Insert\",\"params\":[0,0,{\"file\":\"" + playlistId + "\"}],\"id\":11}");
            HttpGet(url + "/jsonrpc?request={\"jsonrpc\":\"2.0\",\"method\":\"Player.Open\",\"params\":{\"item\":{\"position\":0,\"playlistid\":0},\"options\":{}},\"id\":12}");
            //[{"jsonrpc":"2.0","method":"Playlist.Clear","params":[0],"id":651}]
            //[{"jsonrpc":"2.0","method":"Playlist.Insert","params":[0,0,{"directory":"special://profile/playlists/music/CustomPlaylist.m3u"}],"id":682}]
            //[{"jsonrpc":"2.0","method":"Player.Open","params":{"item":{"position":0,"playlistid":0},"options":{}},"id":814}]
        }
        /*
        * Helper Functions
        */
        private static string HttpGet(string url)
        {
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.Timeout = 4000;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {

                    html = reader.ReadToEnd();



                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    // Handle timeout exception
                }
                else throw;
            }
            return html;
        }
    }
}
