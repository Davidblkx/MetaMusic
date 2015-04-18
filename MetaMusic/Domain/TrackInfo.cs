using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaMusic.Domain
{

    public class TrackInfo
    {
        /// <summary>
        /// Track number
        /// </summary>
        public int Track { get; set; }
        /// <summary>
        /// Track title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Track musicbrainz id
        /// </summary>
        public string Mbid { get; set; }
        /// <summary>
        /// Album musicbrainz id
        /// </summary>
        public string AlbumMbid { get; set; }
        /// <summary>
        /// Artist musicbrainz id
        /// </summary>
        public string ArtistMbid { get; set; }
        /// <summary>
        /// Playcount according to last.fm
        /// </summary>
        public string PlayCount { get; set; }
        /// <summary>
        /// number of listeners according to last.fm
        /// </summary>
        public string Listeners { get; set; }
    }
}
