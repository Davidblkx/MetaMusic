using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MetaMusic.API.Common;
using MetaMusic.Helpers;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.LastFm
{
    public class LastFmAgent
    {
        /// <summary>
        /// Lastfm API 2.0 Domain URL
        /// </summary>
        public readonly string Domain = @"http://ws.audioscrobbler.com/2.0/";
        /// <summary>
        /// User Credential in string format
        /// </summary>
        private readonly string _credentials;

        /// <summary>
        /// Default Constructor 
        /// </summary>
        /// <param name="auth">Auth Credentials for last.fm</param>
        public LastFmAgent(AuthCredentials auth)
        {
            if(auth == null)
                throw new ArgumentNullException("auth");

            _credentials = string.Format("api_key={0}", auth.ApiKey);
        }

        /// <summary>
        /// Get artist by MusicBrainz ID (Avoid using this)
        /// </summary>
        /// <param name="mbid">MusicBrainz ID</param>
        /// <returns></returns>
        public async Task<LastFmArtist> GetArtistbyMbid(string mbid)
        {
            string args = string.Format("?method=artist.getinfo&mbid={0}&{1}&format=json", mbid, _credentials);
            Uri url = new Uri(Domain + args);

            HttpClient webClient = new HttpClient();

            string jsonSource = await  webClient.GetStringAsync(url);
            JObject json = JObject.Parse(jsonSource);
            JToken temp;

            if (json.TryGetValue("error", out temp))
            {
                LastFmApiExceptionArgs arg = new LastFmApiExceptionArgs(temp.ToObject<int>(), 
                    json.GetValue("message").ToString());
                throw new LastFmApiException(arg);
            }

            LastFmArtist art = LastFmArtist.Parse(json);
            return art;
        }
        /// <summary>
        /// Get album by MusicBrainz ID (Avoid using this)
        /// </summary>
        /// <param name="mbid">MusicBrainz ID</param>
        /// <returns></returns>
        public async Task<LastFmAlbum> GetAlbumByMbid(string mbid)
        {
            string args = string.Format("?method=album.getinfo&mbid={0}&{1}&format=json", mbid, _credentials);
            Uri url = new Uri(Domain + args);

            HttpClient webClient = new HttpClient();

            string jsonSource = await webClient.GetStringAsync(url);
            JObject json = JObject.Parse(jsonSource);
            JToken temp;

            if (json.TryGetValue("error", out temp))
            {
                LastFmApiExceptionArgs arg = new LastFmApiExceptionArgs(temp.ToObject<int>(),
                    json.GetValue("message").ToString());
                throw new LastFmApiException(arg);
            }

            LastFmAlbum album = LastFmAlbum.Parse(json);
            return album;
        }
        /// <summary>
        /// Get Track by MusicBrainz ID (Avoid using this)
        /// </summary>
        /// <param name="mbid">MusicBrainz ID</param>
        /// <returns></returns>
        public async Task<LastFmTrack> GetTrackByMbid(string mbid)
        {
            string args = string.Format("?method=track.getInfo&mbid={0}&{1}&format=json", mbid, _credentials);
            Uri url = new Uri(Domain + args);

            HttpClient webClient = new HttpClient();

            string jsonSource = await webClient.GetStringAsync(url);
            JObject json = JObject.Parse(jsonSource);
            JToken temp;

            if (json.TryGetValue("error", out temp))
            {
                LastFmApiExceptionArgs arg = new LastFmApiExceptionArgs(temp.ToObject<int>(),
                    json.GetValue("message").ToString());
                throw new LastFmApiException(arg);
            }

            LastFmTrack album = LastFmTrack.Parse(json);
            return album;
        }


        public async Task<IList<LastFmSearchArtistResult>> SearchArtist(string artistName)
        {
            string args = $"?method=artist.search&artist={artistName}&{_credentials}&format=json";
            Uri url = new Uri(Domain + args);

            HttpClient webClient = new HttpClient();

            string jsonSource = await webClient.GetStringAsync(url);
            JObject json = JObject.Parse(jsonSource);
            JToken temp;

            if (json.TryGetValue("error", out temp))
            {
                LastFmApiExceptionArgs arg = new LastFmApiExceptionArgs(temp.ToObject<int>(),
                    json.GetValue("message").ToString());
                throw new LastFmApiException(arg);
            }

            var resuts = ParseSearchArtistResults(json);
            return resuts;
        }
        public async Task<IList<LastFmSearchAlbumResult>> SearchAlbum(string albumName)
        {
            string args = $"?method=album.search&album={albumName}&{_credentials}&format=json";
            Uri url = new Uri(Domain + args);

            HttpClient webClient = new HttpClient();

            string jsonSource = await webClient.GetStringAsync(url);
            JObject json = JObject.Parse(jsonSource);
            JToken temp;

            if (json.TryGetValue("error", out temp))
            {
                LastFmApiExceptionArgs arg = new LastFmApiExceptionArgs(temp.ToObject<int>(),
                    json.GetValue("message").ToString());
                throw new LastFmApiException(arg);
            }

            var resuts = ParseSearchAlbumResults(json);
            return resuts;
        }
        public async Task<IList<LastFmSearchTrackResult>> SearchTrack(string trackTitle, string trackArtist)
        {
            string args = $"?method=track.search&track={trackTitle}&artist={trackArtist}&{_credentials}&format=json";
            Uri url = new Uri(Domain + args);

            HttpClient webClient = new HttpClient();

            string jsonSource = await webClient.GetStringAsync(url);
            JObject json = JObject.Parse(jsonSource);
            JToken temp;

            if (json.TryGetValue("error", out temp))
            {
                LastFmApiExceptionArgs arg = new LastFmApiExceptionArgs(temp.ToObject<int>(),
                    json.GetValue("message").ToString());
                throw new LastFmApiException(arg);
            }

            var resuts = ParseSearchTrackResults(json);
            return resuts;
        }

        private static IList<LastFmSearchArtistResult> ParseSearchArtistResults(JObject jData)
        {
            JToken results = jData["results"]["artistmatches"];
            List<LastFmSearchArtistResult> list = new List<LastFmSearchArtistResult>();

            foreach (var artist in results["artist"])
            {
                List<ImageInfo> images = new List<ImageInfo>();
                foreach (var img in artist["image"])
                {
                    ImageInfoSizes size;
                    Enum.TryParse(img["size"].ToString(), true, out size);
                    images.Add(new ImageInfo
                    {
                        Size = new ImageSize(size),
                        Link = img["#text"].ToString()
                    });
                }

                list.Add(new LastFmSearchArtistResult
                {
                    Images = images,
                    Listeners = artist["listeners"].ToString(),
                    Mbid = artist["mbid"].ToString(),
                    Name = artist["name"].ToString(),
                    Url = artist["url"].ToString()
                });
            }

            return list;
        }
        private static IList<LastFmSearchAlbumResult> ParseSearchAlbumResults(JObject jData)
        {
            JToken data = jData["results"]["albummatches"];
            var albums = new List<LastFmSearchAlbumResult>();

            foreach (var alb in data["album"])
            {
                List<ImageInfo> images = new List<ImageInfo>();
                foreach (var img in alb["image"])
                {
                    ImageInfoSizes size;
                    Enum.TryParse(img["size"].ToString(), true, out size);
                    images.Add(new ImageInfo
                    {
                        Size = new ImageSize(size),
                        Link = img["#text"].ToString()
                    });
                }

                albums.Add(new LastFmSearchAlbumResult
                {
                    Artist = alb["artist"].ToString(),
                    Id = alb["id"].ToString(),
                    Images = images,
                    Mbid = alb["mbid"].ToString(),
                    Name = alb["name"].ToString(),
                    Url = alb["url"].ToString()
                });
            }

            return albums;
        }
        private static IList<LastFmSearchTrackResult> ParseSearchTrackResults(JObject jData)
        {
            JToken data = jData["results"]["trackmatches"];
            var tracks = new List<LastFmSearchTrackResult>();
            foreach (var trk in data["track"])
            {
                tracks.Add(new LastFmSearchTrackResult
                {
                    Artist = trk.GetStringValue("artist"),
                    Listeners = trk.GetStringValue("listeners"),
                    Mbid = trk.GetStringValue("mbid"),
                    Name = trk.GetStringValue("name"),
                    Url = trk.GetStringValue("url")
                });
            }

            return tracks;
        }


    }
}
