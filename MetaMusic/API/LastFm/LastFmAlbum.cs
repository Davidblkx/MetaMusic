using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaMusic.Domain;
using MetaMusic.Helpers;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.LastFm
{
    public class LastFmAlbum
    {
        public string Title { get; set; }
        public string Artist { get; set; }

        public string LastFmId { get; set; }
        public string Mbid { get; set; }

        public string Url { get; set; }
        public string Release { get; set; }

        public IList<ImageInfo> Images { get; set; }

        public LastFmStats Stats { get; set; }

        public IList<LastFmAlbumTrack> Tracks { get; set; }

        public IList<string> Tags { get; set; }

        public static LastFmAlbum Parse(JObject jObject)
        {
            LastFmAlbum alb = new LastFmAlbum();
            JToken data = jObject["album"];

            alb.Title = data.GetStringValue("name");
            alb.Artist = data.GetStringValue("artist");
            alb.LastFmId = data.GetStringValue("id");
            alb.Mbid = data.GetStringValue("mbid");
            alb.Url = data.GetStringValue("url");
            alb.Release = data.GetStringValue("releasedate");

            alb.Stats = new LastFmStats
            {
                Listeners = data.GetStringValue("listeners"),
                PlayCount = data.GetStringValue("playcount")
            };

            alb.Images = new List<ImageInfo>();
            if (data.HasProperty("image"))
            {
                foreach (var img in data["image"])
                {
                    ImageInfoSizes size = ImageInfoSizes.Small;
                    Enum.TryParse(img.Value<string>("size"), true, out size);

                    alb.Images.Add(new ImageInfo
                    {
                        ImageInfoSize = size,
                        Link = img.Value<string>("#text"),
                    });
                }
            }

            alb.Tags = new List<string>();
            if (data.HasProperty("toptags"))
            {
                foreach (var tag in data["toptags"]["tag"])
                {
                    alb.Tags.Add(tag["name"].ToString());
                }
            }

            alb.Tracks = new List<LastFmAlbumTrack>();
            if (data.HasProperty("tracks"))
            {
                foreach (var t in data["tracks"]["track"])
                {
                    int r;
                    if (!int.TryParse(t["@attr"]["rank"].ToString(), out r))
                        r = -1;

                    int d;
                    if (!int.TryParse(t["duration"].ToString(), out d))
                        d = 0;

                    alb.Tracks.Add(new LastFmAlbumTrack
                    {
                        Rank = r,
                        AlbumName = alb.Title,
                        ArtistMbid = t["artist"]["mbid"].ToString(),
                        ArtistName = t["artist"]["name"].ToString(),
                        Duration = d,
                        Title = t["name"].ToString()
                    });
                }
            }

            return alb;
        }
    }
}
