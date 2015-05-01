using System.Collections.Generic;
using MetaMusic.API.Common;

namespace MetaMusic.Domain
{
    /// <summary>
    /// Information from artist
    /// </summary>
    public class ArtistInfo
    {
        /// <summary>
        /// Id from MusicBrainz
        /// </summary>
        public string Mbid { get; set; }
        /// <summary>
        /// Id from Discogs
        /// </summary>
        public string Dcid { get; set; }
        /// <summary>
        /// Artist Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List of Artist Alias
        /// </summary>
        public IList<string> Alias { get; set; }

        /// <summary>
        /// Artist Main Albums
        /// </summary>
        public IList<AlbumInfo> MainAlbums { get; set; }
        /// <summary>
        /// Artist All Albums
        /// </summary>
        public IList<AlbumInfo> AllAlbums { get; set; }
        /// <summary>
        /// If group, List of Members
        /// </summary>
        public IList<ArtistMember> Members { get; set; }
        /// <summary>
        /// True if group
        /// </summary>
        public bool IsGroup { get; set; }

        /// <summary>
        /// List of active years and timespan
        /// </summary>
        public IList<ActiveTime> ActiveTimes { get; set; }
        /// <summary>
        /// Formation year
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Artist is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Link to musicbrainz page
        /// </summary>
        public string LinkMusicBrainz { get; set; }
        /// <summary>
        /// Link to discogs page
        /// </summary>
        public string LinkDiscogs { get; set; }
        /// <summary>
        /// Link to Last.FM page
        /// </summary>
        public string LinkLastFm { get; set; }

        /// <summary>
        /// If on Tour
        /// </summary>
        public bool OnTour { get; set; }
        /// <summary>
        /// Number of Listeners
        /// </summary>
        public string ListenersCount { get; set; }
        /// <summary>
        /// Number of Plays
        /// </summary>
        public string PlayCount { get; set; }
        /// <summary>
        /// Main tags
        /// </summary>
        public IList<string> Tags { get; set; }
        /// <summary>
        /// Images sources
        /// </summary>
        public IList<ImageInfo> Images { get; set; }
    }
}
