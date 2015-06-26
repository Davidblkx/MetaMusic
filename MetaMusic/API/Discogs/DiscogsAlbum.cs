using System.Collections.Generic;
using System.Linq;
using MetaMusic.API.Common;
using MetaMusic.Helpers;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.Discogs
{
    public class DiscogsAlbum
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string MasterId { get; set; }

        public bool IsMaster
        {
            get { return Id == MasterId; }
        }

        public IList<DiscogsAlbumArtist> Artists { get; set; }
        public IList<DiscogsTrack> TrackList { get; set; }
        public IList<ImageInfo> Images { get; set; }

        public IList<string> Styles { get; set; }
        public IList<string> Genres { get; set; }

        public int Year { get; set; }

        public static DiscogsAlbum ParseAlbum(JObject jData)
        {
            DiscogsAlbum album = new DiscogsAlbum
            {
                Name = jData.GetStringValue("title").FixDiscogsName(),
                Id = jData.GetStringValue("id"),
                Year = jData.GetStringValue("year").ToInt(),
                Artists = new List<DiscogsAlbumArtist>(),
                TrackList = new List<DiscogsTrack>(),
                Images = new List<ImageInfo>(),
                Styles = new List<string>(jData["styles"].Select(x=>x.ToString())),
                Genres = new List<string>(jData["genres"].Select(x=>x.ToString()))
            };

            foreach (var art in jData["artists"])
            {
                album.Artists.Add(new DiscogsAlbumArtist
                {
                    Name = art.GetStringValue("name").FixDiscogsName(),
                    Id = art.GetStringValue("id")
                });
            }

            foreach (var trk in jData["tracklist"])
            {
                album.TrackList.Add(new DiscogsTrack
                {
                    Title = trk.GetStringValue("title"),
                    Duration = trk.GetStringValue("duration"),
                    Position = trk.GetStringValue("position")
                });
            }
            foreach (var img in jData["images"])
            {
                album.Images.Add(new ImageInfo
                {
                    Link = img.GetStringValue("uri"),
                    Size = new ImageSize(img.GetStringValue("width").ToInt(), img.GetStringValue("height").ToInt())
                });
            }

            //Check if master release
            album.MasterId = jData.HasProperty("master_id") ? jData.GetStringValue("master_id") : album.Id;

            return album;
        }
    }
}
