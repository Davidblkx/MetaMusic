using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaMusic.Domain
{
    /// <summary>
    /// Main album info
    /// </summary>
    public class AlbumInfo
    {
        /// <summary>
        /// Album title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Release year
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Discogs
        /// </summary>
        public int MasterId { get; set; }
        /// <summary>
        /// Musicbrainz release-group id
        /// </summary>
        public string Mbid { get; set; }
        /// <summary>
        /// Number of listeners according to Last.FM
        /// </summary>
        public string ListenersCount { get; set; }
        /// <summary>
        /// Number of play counts according to Last.FM
        /// </summary>
        public string PlayCount { get; set; }
        /// <summary>
        /// Track list
        /// </summary>
        public IList<TrackInfo> Tracks { get; set; }
        /// <summary>
        /// Album covers
        /// </summary>
        public IList<ImageInfo> Covers { get; set; }
    }
}
