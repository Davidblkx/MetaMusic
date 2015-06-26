using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MetaMusic.API.Common;
using MetaMusic.API.LastFm;
using MetaMusic.Helpers;
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
        public async Task<DiscogsAlbum> GetMasterAlbumById(string id)
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
        public async Task<DiscogsAlbum> GetReleaseAlbumById(string id)
        {
            string args = string.Format("/releases/{0}?{1}", id, _credentials);
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
        public async Task<List<DiscogsReleaseVersion>> GetReleases(string masterId)
        {
            try
            {
                var list = new List<DiscogsReleaseVersion>();
                int page = 1;
                int maxPage = 99;

                do
                {
                    string args = string.Format("/masters/{0}/versions?{1}&per_page=100&page={2}", masterId,
                        _credentials, page);
                    Uri url = new Uri(Domain + args);

                    HttpClient webClient = CreateDiscogsClient();
                    string jsonSource = await webClient.GetStringAsync(url);

                    JObject json = JObject.Parse(jsonSource);

                    page = json["pagination"].GetStringValue("page").ToInt();
                    maxPage = json["pagination"].GetStringValue("pages").ToInt();

                    if (json.HasProperty("versions"))
                    {
                        list.AddRange(json["versions"].Select(DiscogsReleaseVersion.Parse));
                    }

                    page++;

                } while (page <= maxPage);

                return list;

            }
            catch (JsonReaderException jException)
            {
                throw new Exception(jException.Message, jException);
            }
            catch (Exception ex)
            {
                throw new Exception("A error as occur", ex);
            }
        }

        public async Task<List<DiscogsSearchArtistResult>> SearchArtist(string artist)
        {
            try
            {
                var list = new List<DiscogsSearchArtistResult>();
                int page = 1;
                int maxPage = 99;

                do
                {
                    string args = string.Format("/database/search?q={0}&per_page=100&page={2}&{1}&type=artist", artist,
                        _credentials, page);
                    Uri url = new Uri(Domain + args);

                    HttpClient webClient = CreateDiscogsClient();
                    string jsonSource = await webClient.GetStringAsync(url);

                    JObject json = JObject.Parse(jsonSource);

                    page = json["pagination"].GetStringValue("page").ToInt();
                    maxPage = json["pagination"].GetStringValue("pages").ToInt();

                    if (json.HasProperty("results"))
                    {
                        list.AddRange(json["results"].Select(DiscogsSearchArtistResult.Parse));
                    }

                    page++;

                } while (page <= maxPage);

                return list;

            }
            catch (JsonReaderException jException)
            {
                throw new Exception(jException.Message, jException);
            }
            catch (Exception ex)
            {
                throw new Exception("A error as occur", ex);
            }
        }
        public async Task<List<DiscogsSearchAlbumResult>> SearchMasterAlbum(string album, string artist)
        {
            try
            {
                var list = new List<DiscogsSearchAlbumResult>();
                int page = 1;
                int maxPage = 99;

                do
                {
                    string args = string.Format("/database/search?q={0}&per_page=100&page={3}&{1}&artist={2}&format=album&type=master", album,
                        _credentials, artist, page);
                    Uri url = new Uri(Domain + args);

                    HttpClient webClient = CreateDiscogsClient();
                    string jsonSource = await webClient.GetStringAsync(url);

                    JObject json = JObject.Parse(jsonSource);

                    page = json["pagination"].GetStringValue("page").ToInt();
                    maxPage = json["pagination"].GetStringValue("pages").ToInt();

                    if (json.HasProperty("results"))
                    {
                        list.AddRange(json["results"].Select(DiscogsSearchAlbumResult.Parse));
                    }

                    page++;

                } while (page <= maxPage);

                return list;

            }
            catch (JsonReaderException jException)
            {
                throw new Exception(jException.Message, jException);
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
