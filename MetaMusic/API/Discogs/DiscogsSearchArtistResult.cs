using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaMusic.Helpers;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.Discogs
{
    public class DiscogsSearchArtistResult
    {
        public string Thumb { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }

        public static DiscogsSearchArtistResult Parse(JToken jData)
        {
            return new DiscogsSearchArtistResult
            {
                Thumb = jData.GetStringValue("thumb"),
                Name = jData.GetStringValue("title"),
                Id = jData.GetStringValue("id")
            };
        }
    }
}
