using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaMusic.API.Common;
using MetaMusic.Domain;

namespace MetaMusic.API.LastFm
{
    public class LastFmSearchAlbumResult
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Mbid { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public IList<ImageInfo> Images { get; set; }
    }
}
