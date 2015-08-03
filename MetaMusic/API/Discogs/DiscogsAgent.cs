using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MetaMusic.API.Common;
using MetaMusic.API.LastFm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.Discogs
{
    public class DiscogsAgent
    {
        public readonly string Domain;
        private readonly string _credentials;
        public readonly string UserAgent;

        /// <summary>
        /// Milliseconds between calls (Discogs as a 20 requests per minute limite so 3000 is the default value)
        /// </summary>
        public int RequestInterval { get; set; }

        /// <summary>
        /// Last request time
        /// </summary>
        private static DateTime _lastReq = new DateTime(1990, 3, 1);

        public DiscogsAgent(DiscogsAuth auth)
        {
            Domain = "https://api.discogs.com";
            UserAgent = auth.UserAgent;
            _credentials = $"key={auth.ApiKey}&secret={auth.ApiSecret}";
            RequestInterval = 3000;
        }

        public async Task<DiscogsArtist> GetArtistById(string id)
        {
            string args = $"/artists/{id}?{_credentials}";

            var jsonSource = await GetRequestJson(args);

            try
            {
                JObject json = JObject.Parse(jsonSource);
                DiscogsArtist art = DiscogsArtist.ParseArtist(json);
                return art;
            }
            catch (JsonReaderException jException)
            {
                throw new Exception(jsonSource, jException);
            }
            catch (Exception ex)
            {
                throw new Exception("A error as occur", ex);
            }
        }
        public async Task<DiscogsAlbum> GetAlbumById(string id)
        {
            string args = $"/masters/{id}?{_credentials}";

            var jsonSource = await GetRequestJson(args);

            try
            {
                JObject json = JObject.Parse(jsonSource);
                DiscogsAlbum album = DiscogsAlbum.ParseAlbum(json);
                return album;
            }
            catch (JsonReaderException jException)
            {
                throw new Exception(jsonSource, jException);
            }
            catch (Exception ex)
            {
                throw new Exception("A error as occur", ex);
            }
        }

        public async Task<IList<DiscogsSearchArtistResult>> SearchArtist(string artistNameQuery)
        {
            string args = $"/database/search?{_credentials}&q={artistNameQuery}&type=artist&per_page=100";

            var jsonSource = await GetRequestJson(args);

            try
            {
                JObject json = JObject.Parse(jsonSource);
                var results = DiscogsSearchArtistResult.ParseSearchResult(json, artistNameQuery);
                return results;
            }
            catch (JsonReaderException jException)
            {
                throw new Exception(jsonSource, jException);
            }
            catch (Exception ex)
            {
                throw new Exception("A error as occur", ex);
            }
        }

        private HttpClient CreateDiscogsClient()
        {
            HttpClient webClient = new HttpClient(new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                AllowAutoRedirect = false,
            });
            webClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            return webClient;
        }

        private async Task<string> GetRequestJson(string args)
        {
            var url = new Uri(Domain + args);
            var webClient = CreateDiscogsClient();

        
            var spentTime = GetAwaitTime();
            Debug.WriteLine("Spent time = " + spentTime);
            if (spentTime > 0 && spentTime < RequestInterval)
                await Task.Delay(RequestInterval - spentTime);

            var jsonSource = await webClient.GetStringAsync(url);

            _lastReq = DateTime.Now;

            return jsonSource;
        }

        /// <summary>
        /// Returns the time spent since last request
        /// </summary>
        /// <returns></returns>
        private int GetAwaitTime()
        {
            DateTime now = DateTime.Now;
            TimeSpan result = now.Subtract(_lastReq);
            var value = result.TotalMilliseconds;
            if (value > int.MaxValue)
                value = int.MaxValue;
            return (int) value;
        }
    }
}
