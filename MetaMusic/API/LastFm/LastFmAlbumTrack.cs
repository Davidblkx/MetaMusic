using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaMusic.API.LastFm
{
    public class LastFmAlbumTrack
    {
        public int Rank { get; set; }
        public int Duration { get; set; }

        public string Title { get; set; }

        public string AlbumName { get; set; }

        public string ArtistName { get; set; }
        public string ArtistMbid { get; set; }
    }
}
