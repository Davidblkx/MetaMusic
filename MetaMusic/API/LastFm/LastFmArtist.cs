using System;
using System.Collections.Generic;
using MetaMusic.Helpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaMusic.Domain;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.LastFm
{
    public class LastFmArtist
    {
        public string Name { get; set; }
        public string Mbid { get; set; }
        public string Url { get; set; }
        public IList<ImageInfo> Images { get; set; }
        public bool Streamable { get; set; }
        public bool OnTour { get; set; }
        public LastFmStats Stats { get; set; }
        public IList<string> Tags { get; set; }
        public int Year { get; set; }
        public IList<LastFmMembers> Members { get; set; }

        public static LastFmArtist Parse(JObject jsonObj)
        {
            LastFmArtist art = new LastFmArtist();
            JToken artToken = jsonObj["artist"];

            art.Name = artToken.HasProperty("name") ? artToken.Value<string>("name") : "Undefined";
            art.Mbid = artToken.HasProperty("mbid") ? artToken.Value<string>("mbid") : "Undefined";

            art.Members = new List<LastFmMembers>();
            if (artToken.HasProperty("bandmembers"))
            {
                foreach (JToken m in artToken["bandmembers"]["member"].Children())
                {
                    art.Members.Add(new LastFmMembers
                    {
                        Name = m.Value<string>("name"),
                        YearFrom = m.Value<int>("yearfrom"),
                        YearTo = m.Value<int>("yearto")
                    });
                }
            }

            art.Url = artToken.HasProperty("url") ? artToken.Value<string>("url") : "Undefined";
            art.Streamable = artToken.HasProperty("streamable") && artToken.Value<string>("streamable") != "0";
            art.OnTour = artToken.HasProperty("ontour") && artToken.Value<string>("ontour") != "0";

            art.Images = new List<ImageInfo>();
            if (artToken.HasProperty("image"))
            {
                foreach (var img in artToken["image"])
                {
                    ImageInfoSizes size = ImageInfoSizes.Small;
                    Enum.TryParse(img.Value<string>("size"), true, out size);

                    art.Images.Add(new ImageInfo
                    {
                        ImageInfoSize = size,
                        Link = img.Value<string>("#text"),
                    });
                }
            }

            art.Stats = new LastFmStats();
            if(artToken.HasProperty("stats"))
            {
                art.Stats.Listeners = artToken["stats"].HasProperty("listeners")
                    ? artToken["stats"].Value<string>("listeners")
                    : "0";
                art.Stats.PlayCount = artToken["stats"].HasProperty("playcount")
                    ? artToken["stats"].Value<string>("playcount")
                    : "0";
            };

            art.Tags = new List<string>();
            if (artToken.HasProperty("tags"))
            {
                foreach (var tag in artToken["tags"]["tag"])
                {
                    art.Tags.Add(tag["name"].ToString());
                }
            }

            if (artToken.HasProperty("bio"))
                art.Year = int.Parse(artToken["bio"]["yearformed"].ToString());

            return art;
        }
    }
}
