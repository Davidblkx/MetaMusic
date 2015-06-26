using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaMusic.Helpers;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.Discogs
{
    public class DiscogsSearchAlbumResult
    {
        public string Thumb { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }

        public static DiscogsSearchAlbumResult Parse(JToken jData)
        {
            return new DiscogsSearchAlbumResult
            {
                Id = jData.GetStringValue("id"),
                Name = jData.GetStringValue("title"),
                Thumb = jData.GetStringValue("thumb"),
                Year = jData.GetStringValue("year")
            };
        }
    }
}
