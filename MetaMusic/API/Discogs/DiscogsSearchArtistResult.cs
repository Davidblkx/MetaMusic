using System.Collections;
using System.Collections.Generic;
using MetaMusic.Helpers;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.Discogs
{
    public class DiscogsSearchArtistResult
    {
        public string Id { get; set; }
        /// <summary>
        /// Artist Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Value calculated from LevenshteinDistance algorithm 
        /// </summary>
        public int ProxValue { get; set; }
        public string Thumb { get; set; }

        /// <summary>
        /// Parse json data from search result
        /// </summary>
        /// <param name="jsonData">Json data object</param>
        /// <param name="query">Search query</param>
        /// <returns></returns>
        public static IList<DiscogsSearchArtistResult> ParseSearchResult(JToken jsonData, string query)
        {
            List<DiscogsSearchArtistResult> list = new List<DiscogsSearchArtistResult>();

            foreach (JToken resjJToken in jsonData["results"])
            {
                string nm = resjJToken.GetStringValue("title").FixDiscogsName();
                list.Add(new DiscogsSearchArtistResult
                {
                    Id = resjJToken.GetStringValue("id"),
                    Name = nm,
                    Thumb = resjJToken.GetStringValue("thumb"),
                    ProxValue = Algorithms.LevenshteinDistance(query.ToLower(), nm.ToLower())
                });
            }

            return list;
        }
    }
}
