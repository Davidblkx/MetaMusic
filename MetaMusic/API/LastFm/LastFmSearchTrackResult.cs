using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaMusic.API.LastFm
{
    public class LastFmSearchTrackResult
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Url { get; set; }
        public string Mbid { get; set; }
        public string Listeners { get; set; }
    }
}
