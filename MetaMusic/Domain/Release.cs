using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaMusic.Domain
{
    /// <summary>
    /// Album release
    /// </summary>
    public class Release
    {
        /// <summary>
        /// Source: Discogs or Musicbrainz
        /// </summary>
        public ReleaseSource Source { get; set; }
        /// <summary>
        /// Discogs master ID
        /// </summary>
        public string MasterId { get; set; }
        /// <summary>
        /// MusicBrains release group ID
        /// </summary>
        public string AlbumMbId { get; set; }
        /// <summary>
        /// Release Date
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Release track list
        /// </summary>
        public IList<TrackInfo> Tracks { get; set; }
    }
}
