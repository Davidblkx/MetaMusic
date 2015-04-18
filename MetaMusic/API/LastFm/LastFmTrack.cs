using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaMusic.Helpers;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.LastFm
{
    public class LastFmTrack
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Mbid { get; set; }
        public string Url { get; set; }
        public string Duration { get; set; }
        public LastFmStats Stats { get; set; }

        public string ArtistName { get; set; }
        public string ArtistMbid { get; set; }

        public string AlbumArtist { get; set; }
        public string AlbumTitle { get; set; }
        public string AlbumMbid { get; set; }

        public int Rank { get; set; }

        public IList<string> Tags { get; set; }

        public static LastFmTrack Parse(JObject json)
        {
            LastFmTrack track = new LastFmTrack();
            JToken token = json["track"];

            track.Id = token.GetStringValue("id");
            track.Title = token.GetStringValue("name");
            track.Mbid = token.GetStringValue("mbid");
            track.Stats = new LastFmStats
            {
                Listeners = token.GetStringValue("listeners"),
                PlayCount = token.GetStringValue("playcount")
            };

            track.ArtistName = token["artist"].GetStringValue("name");
            track.ArtistMbid = token["artist"].GetStringValue("mbid");

            track.AlbumArtist = token["album"].GetStringValue("artist");
            track.AlbumTitle = token["album"].GetStringValue("title");
            track.AlbumMbid = token["album"].GetStringValue("mbid");

            int r;
            if (!int.TryParse(token["album"]["@attr"].GetStringValue("position"), out r))
                r = -1;

            track.Rank = r;

            track.Duration = token.GetStringValue("duration");
            track.Url = token.GetStringValue("url");

            track.Tags=new List<string>();
            if (!token.HasProperty("toptags")) return track;
            foreach (var t in token["toptags"]["tag"])
            {
                track.Tags.Add(t.GetStringValue("name"));
            }

            return track;
        }
    }
}
