using System;
using System.Collections.Generic;
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

        public DiscogsAgent(DiscogsAuth auth)
        {
            Domain = "https://api.discogs.com";
            UserAgent = auth.UserAgent;
            _credentials = string.Format("key={0}&secret={1}", auth.ApiKey, auth.ApiSecret);
        }

        public async Task<DiscogsArtist> GetArtistById(string id)
        {
            string args = string.Format("/artists/{0}?{1}", id, _credentials);
            Uri url = new Uri(Domain + args);


            HttpClient webClient = CreateDiscogsClient();
            string jsonSource = await webClient.GetStringAsync(url);

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
            string args = string.Format("/masters/{0}?{1}", id, _credentials);
            Uri url = new Uri(Domain + args);


            HttpClient webClient = CreateDiscogsClient();
            string jsonSource = await webClient.GetStringAsync(url);

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
    }
}
