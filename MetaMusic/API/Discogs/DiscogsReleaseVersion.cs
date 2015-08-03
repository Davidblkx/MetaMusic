using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaMusic.Helpers;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.Discogs
{
    public class DiscogsReleaseVersion
    {
        public bool IsAccepted { get; set; }
        public IList<string> Formats { get; set; }
        public string Country { get; set; }
        public string Released { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }

        public static DiscogsReleaseVersion Parse(JToken jData)
        {
            DiscogsReleaseVersion version = new DiscogsReleaseVersion
            {
                IsAccepted = jData.GetStringValue("status") == "Accepted",
                Country = jData.GetStringValue("country"),
                Title = jData.GetStringValue("title"),
                Released = jData.GetStringValue("released"),
                Id = jData.GetStringValue("id"),
                Formats = jData.GetStringValue("format").Split(',').Select(x => x.Replace(" ", "")).ToList()
            };

            return version;
        }
    }
}
