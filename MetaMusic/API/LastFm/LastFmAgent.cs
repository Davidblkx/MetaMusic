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
        public readonly string Domain = @"http://ws.audioscrobbler.com/2.0/";
        private readonly string _credentials;

        public LastFmAgent(AuthCredentials auth)
        {
            if(auth == null)
                throw new ArgumentNullException("auth");

            _credentials = string.Format("api_key={0}", auth.ApiKey);
        }

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
            string args = string.Format("?method=artist.search&artist={0}&{1}&format=json", artistName, _credentials);
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
            string args = string.Format("?method=album.search&album={0}&{1}&format=json", albumName, _credentials);
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
            string args = string.Format("?method=track.search&track={0}&artist={1}&{2}&format=json", 
                trackTitle, trackArtist, _credentials);
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
